using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

public class TCPConnectManager : MonoBehaviour
{
    public static TCPConnectManager Instance = null;


    [Header("Commons")]
    public GameObject LobbyScene;
    public GameObject ChannelScene;
    public LobbyManager lobbyManagerScript;
    public LoginManager loginManagerScript;
    public ChannelManager channelManagerScript;

    [Header("Chat")]
    public TMP_Text MessageElement;   // 채팅 메시지
    public GameObject LobbyChattingList; // 로비 채팅 리스트
    public GameObject ChannelChattingList; // 채널 채팅 리스트
    public TMP_InputField LobbyChat;  // 로비 입력 메시지
    public TMP_InputField ChannelChat;  // 채널 입력 메시지
    public Button LobbyChatSendBtn;     // 로비 메시지 전송버튼
    public Button ChannelChatSendBtn;   // 채널 메시지 전송 버튼

    [Header("Channel")]
    public GameObject ScrollViewChannelList;    // 채널 리스트
    public GameObject ChannelListElement;       // 채널 리스트 항목
    public ChannelListElement SelectedChannel;  // 선택된 채널 리스트



    [Header("Connect")]
    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private StreamReader reader;
    private StreamWriter writer;
    private User loginUserInfo;

    private string hostname = "j10c211.p.ssafy.io";
    //private string hostname = "192.168.100.107";
    //private string hostname = "172.30.1.26";
    private int port = 1370;

    private void Awake()
    {
        // 싱글톤
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 기존 데이터 instance로 복사
            Instance.LobbyScene = LobbyScene;
            Instance.ChannelScene = ChannelScene;
            Instance.lobbyManagerScript = lobbyManagerScript;
            Instance.loginManagerScript = loginManagerScript;
            Instance.channelManagerScript = channelManagerScript;
            Instance.MessageElement = MessageElement;
            Instance.LobbyChattingList = LobbyChattingList;
            Instance.ChannelChattingList = ChannelChattingList;
            Instance.LobbyChat = LobbyChat;
            Instance.ChannelChat = ChannelChat;
            Instance.LobbyChatSendBtn = LobbyChatSendBtn;
            Instance.ChannelChatSendBtn = ChannelChatSendBtn;
            Instance.ScrollViewChannelList = ScrollViewChannelList;
            Instance.ChannelListElement = ChannelListElement;
            Destroy(gameObject);
        }

        // 리스너 붙이기
        Instance.LobbyChatSendBtn.onClick.RemoveAllListeners();
        Instance.ChannelChatSendBtn.onClick.RemoveAllListeners();
        Instance.LobbyChatSendBtn.onClick.AddListener(Instance.MessageSendBtnClicked);
        Instance.ChannelChatSendBtn.onClick.AddListener(Instance.MessageSendBtnClicked);

        // 만약 로그인 데이터가 있다면 로비매니저 켜기
        if(DataManager.Instance.loginUserInfo.UserId != 0)
        {
            lobbyManagerScript.gameObject.SetActive(true);
        }
    }

    //private void OnEnable()
    //{
    //    // TCP 첫 연결 후에는 요청 2번 받아서 channel, session 2가지 목록을 받아온다.
    //    ConnectToServer();
    //}

    private void OnDisable()
    {
        // 접속관련 메모리 해제
        OnApplicationQuit();
    }

    // ============================= 종료 관련 =============================
    // 종료 시
    public void OnApplicationQuit()
    {
        ResultManager.isResultManager = false;
        // 채팅내역 지우기
        clearChat();

        // tcp연결 관련
        if (_tcpClient != null)
        {
            DisconnectFromServer();
        }
        writer = null;
        loginUserInfo = null;
    }

    public void DisconnectFromServer()
    {
        // 연결 종료
        _networkStream.Close();
        _tcpClient.Close();
        _networkStream = null;
        _tcpClient = null;
    }

    private void Update()
    {
        // 데이터가 들어온 경우
        while (_networkStream != null && _networkStream.DataAvailable)
        {
            string response = ReadMessageFromServer();
            DispatchResponse(response);
        }
        //if (_networkStream.DataAvailable)
        //{
        //    string response = ReadMessageFromServer();
        //    DispatchResponse(response);
        //}

        // 채팅 입력 엔터
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
             MessageSendBtnClicked();
        }
    }

    // 요청 분배하기
    private void DispatchResponse(string response)
    {
        string type = getType(response);

        //Debug.Log("response: " + response);
        //Debug.Log("type: " + type);

        if (type == "chat")  // 채팅 메시지
        {
            showMessage(response, LobbyScene.activeSelf);
        }
        else if (type == "channelList")  // 전체 생성된 방 목록
        {
            setChannelList(response);
        }
        else if (type == "sessionList")  // 전체 접속한 유저 목록
        {
            lobbyManagerScript.setAllUsers(response);
        }
        else if (type == "channelSessionList")   // 방 안의 유저 목록
        {
            setChannelSessionList(response);
            channelManagerScript.showSessionList();
        }
        else if(type == "channelInfo")  // 채널 정보
        {
            setChannelInfo(response);
            channelManagerScript.showSessionList();
        }
        else if (type == "changePos")    // 다른사람 포지션 변경 시
        {
            otherPlayerMove(response);
        }
        else if(type == "score") // 받은 다른사람 점수 데이터매니저에 저장
        {
            setOtherScores(response);
        }
        else if(type == "clear")    // 누군가가 게임 클리어 시
        {
            gameClearOtherPlayerSet(response);
        }
        //else if(type == "changeArm")    // 팔 변경 시
        //{
        //    otherPlayerChangeArm(response);
        //}
        else
        {
            Debug.Log("Response ELSE!!!: " + response);
        }
    }

    // response 받은 메시지 타입 체크하기
    private string getType(string response)
    {
        string[] words = response.Split('\"');
        return words[3];
    }

    // ============================= 서버 연결 관련 =============================
    public void ConnectToServer()
    {
        try
        {
            // TCP 서버에 연결
            _tcpClient = new TcpClient(hostname, port);
            _networkStream = _tcpClient.GetStream();
            reader = new StreamReader(_networkStream);
            writer = new StreamWriter(_networkStream);

            loginUserInfo = DataManager.Instance.loginUserInfo;   // chk
            Debug.Log("ConnectToServer");

            string json = "{" +
                    "\"channel\":\"lobby\"," +
                    $"\"userName\":\"{loginUserInfo.Name}\"," +
                    "\"data\":{" +
                        "\"type\":\"initial\"," +
                        $"\"userId\":\"{loginUserInfo.UserId}\"" +
                    "}" +
                "}";
            SendMessageToServer(json);

            // channel, session 목록 받아오기
            // TCP 첫 연결 후에는 요청 2번 받아서 channel, session 2가지 목록을 받아온다.
            //string response = ReadMessageFromServer();
            //DispatchResponse(response);

            //string response = ReadMessageFromServer();
            //Debug.Log(response);
        }
        catch (Exception e)
        {
            // 연결 중 오류 발생 시
            Debug.Log($"Failed to connect to the server: {e.Message}");
        }
    }

    // 서버로 메시지 보내기
    private void SendMessageToServer(string message)
    {
        if (_tcpClient == null)
        {
            return;
        }
        //Debug.Log(message);
        writer.WriteLine(message);
        writer.Flush(); // 메시지 즉시 전송
    }

    // 서버에서 메시지 읽기
    private string ReadMessageFromServer()
    {
        if(_tcpClient == null)
        {
            return null;
        }

        try
        {
            // 서버로부터 응답 읽기
            //string response = reader.ReadLine();
            //return response;

            //StringBuilder message = new StringBuilder();
            //while (_networkStream.CanRead)
            //{
            //    int readByte = _networkStream.ReadByte();
            //    if (readByte == -1 || readByte == '\n') // '\n'를 구분자로 사용
            //    {
            //        break;
            //    }
            //    message.Append((char)readByte);
            //}
            //return message.ToString();

            //StringBuilder message = new StringBuilder();
            //using (BinaryReader reader = new BinaryReader(_networkStream, Encoding.UTF8))
            //{
            //    while (_networkStream.DataAvailable)
            //    {
            //        char readChar = reader.ReadChar();
            //        if (readChar == '\n') // '\n'를 구분자로 사용
            //        {
            //            break;
            //        }
            //        message.Append(readChar);
            //    }
            //}
            //return message.ToString();

            StringBuilder message = new StringBuilder();
            BinaryReader reader = new BinaryReader(_networkStream, Encoding.UTF8);
            while (_networkStream.DataAvailable)
            {
                char readChar = reader.ReadChar();
                if (readChar == '\n') // '\n'를 구분자로 사용
                {
                    break;
                }
                message.Append(readChar);
            }
            return message.ToString();
        }
        catch(Exception e)
        {
            Debug.Log("응답 읽기 실패: " + e.Message);
            return null;
        }
    }

    // ============================= 채팅 관련 =============================
    // 메시지 전송 버튼 클릭 시
    public void MessageSendBtnClicked()
    {
        Debug.Log("메시지 전송");
        // 로비 채팅인 경우
        if(LobbyScene.activeSelf)
        {
            string message = LobbyChat.text;

            if (message == "")
            {
                return;
            }

            string json = "{" +
                "\"channel\":\"lobby\"," +
                $"\"userName\":\"{loginUserInfo.Name}\"," +
                "\"data\":{" +
                    "\"type\":\"chat\"," +
                    "\"message\":" + "\"" + message + "\"" +
                "}" +
            "}";

            LobbyChat.text = "";

            SendMessageToServer(json);
            LobbyChat.Select();
            LobbyChat.ActivateInputField();
        }
        // 채널 채팅인 경우
        else
        {
            string message = ChannelChat.text;

            if (message == "")
            {
                return;
            }

            string json = "{" +
                "\"channel\":\"channel\"," +
                $"\"userName\":\"{DataManager.Instance.loginUserInfo.Name}\"," +
                "\"data\":{" +
                    "\"type\":\"chat\"," +
                    $"\"channelIndex\":\"{DataManager.Instance.channelIndex}\"," +
                    "\"message\":" + "\"" + message + "\"" +
                "}" +
            "}";

            ChannelChat.text = "";

            SendMessageToServer(json);
            ChannelChat.Select();
            ChannelChat.ActivateInputField();
        }
    }


    // 메시지 들어왔을 때
    // TODO: 메시지 오브젝트 풀링 적용하기
    public void showMessage(string message, bool isLobby)
    {
        if(isLobby)
        {
            // 붙일 부모 오브젝트
            Transform content = LobbyChattingList.transform.Find("Viewport/Content");

            ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

            // 내가 보낸 메시지 표시하기
            string me = "";
            if (chatMessage.UserName == DataManager.Instance.loginUserInfo.Name)
            {
                me = "(나)";
            }
            
            TMP_Text temp1 = Instantiate(MessageElement);
            //temp1.text = chatMessage.UserName + ": " + chatMessage.Message;
            temp1.text = $"{chatMessage.UserName} {me} : {chatMessage.Message}";
            temp1.transform.SetParent(content, false);

            

            // 20개 넘어가면 채팅 위에서부터 지우기
            // TODO: 오브젝트 풀링
            if(content.childCount >= 20)
            {
                Destroy(content.GetChild(1).gameObject);
            }
        }
        else
        {
            // 붙일 부모 오브젝트
            Transform content = ChannelChattingList.transform.Find("Viewport/Content");

            ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

            // 내가 보낸 메시지 표시하기
            string me = "";
            if (chatMessage.UserName == DataManager.Instance.loginUserInfo.Name)
            {
                me = "(나)";
            }

            TMP_Text temp1 = Instantiate(MessageElement);
            temp1.text = $"{chatMessage.UserName} {me} : {chatMessage.Message}";
            temp1.transform.SetParent(content, false);

            // 20개 넘어가면 채팅 위에서부터 지우기
            // TODO: 오브젝트 풀링
            if (content.childCount >= 20)
            {
                Destroy(content.GetChild(1).gameObject);
            }
        }

        StartCoroutine(ScrollToBottom(isLobby));
    }

    // 스크롤 맨 아래로 내리기
    IEnumerator ScrollToBottom(bool isLobby)
    {
        if(isLobby)
        {
            // 다음 프레임 기다림
            yield return null;

            Transform content = LobbyChattingList.transform.Find("Viewport/Content");

            // Layout Group을 강제로 즉시 업데이트
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);

            // 스크롤 맨 아래로 내림
            LobbyChattingList.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
        }
        else
        {
            // 다음 프레임 기다림
            yield return null;

            Transform content = ChannelChattingList.transform.Find("Viewport/Content");

            // Layout Group을 강제로 즉시 업데이트
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);

            // 스크롤 맨 아래로 내림
            ChannelChattingList.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
        }
    }

    // 채팅내역 지우기
    public void clearChat()
    {
        Transform content;

        if(LobbyChattingList != null)
        {
            content = LobbyChattingList.transform.Find("Viewport/Content");
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }
        
        if(ChannelChattingList != null)
        {
            content = ChannelChattingList.transform.Find("Viewport/Content");
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
        }
    }

    // ============================= 방(channel), 유저(session) 관련 =============================
    [Serializable]
    class ChannelList
    {
        public string type;
        public ChannelInfo[] data;
    }
    [Serializable]
    class ChannelInfo
    {
        public int channelIndex;
        public string channelName;
        public int cnt;
        public bool isOnGame;
    }
    // 방 리스트 불러오기
    public void setChannelList(string response)
    {
        //Debug.Log("들어옴 setChannelList");
        // 채널 리스트 들어갈 스크롤뷰(설정할 부모)
        Transform content = ScrollViewChannelList.transform.Find("Viewport/Content");
        // 기존 리스트 제거
        // TODO: 오브젝트 풀링
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // JSON 파싱
        ChannelList channelList = JsonUtility.FromJson<ChannelList>(response);

        for(int i = 0; i < channelList.data.Length; i++)
        {
            // 참가 인원 꽉차면 안보이게 하기
            if(channelList.data[i].isOnGame || channelList.data[i].cnt == 6)
            {
                continue;
            }

            //Debug.Log("방이름: " + channelList.data[i].channelName);
            // 프리팹 만들기
            GameObject channelListElement = Instantiate(ChannelListElement);
            ChannelListElement channelListElementScript = channelListElement.GetComponent<ChannelListElement>();
            channelListElementScript.ChannelIndex = channelList.data[i].channelIndex;
            channelListElementScript.ChannelName = channelList.data[i].channelName;
            channelListElementScript.Cnt = channelList.data[i].cnt;
            channelListElementScript.IsOnGame = channelList.data[i].isOnGame;

            // 부모 붙이기
            channelListElement.transform.SetParent(content, false);
        }
    }

    // 방 만들기
    public void createChannel(string json)
    {
        //Debug.Log("채널 만들기: " + json);
        SendMessageToServer(json);

        // 방정보 받아오기
        //string response = ReadMessageFromServer();
        //DispatchResponse(response);

        LobbyScene.SetActive(false);
        ChannelScene.SetActive(true);

        //channelManagerScript.gameObject.SetActive(true);
    }

    // 방 정보 설정
    public void setChannelInfo(string response)
    {
        // JSON 파싱
        ChannelList channelList = JsonUtility.FromJson<ChannelList>(response);

        DataManager.Instance.channelIndex = channelList.data[0].channelIndex;
        DataManager.Instance.channelName = channelList.data[0].channelName;
        DataManager.Instance.cnt = channelList.data[0].cnt;
        DataManager.Instance.isOnGame = channelList.data[0].isOnGame;

        // 게임시작
        if (channelList.data[0].isOnGame)
        {
            if(SceneManager.GetActiveScene().name == "Login")
            {
                DataManager.Instance.score = new int[DataManager.Instance.cnt];
                SceneManager.LoadScene("Intro");
            }
        }
        // 채널 내에서(게임시작x)
        else
        {
            Debug.Log(channelList.data[0].channelName);
            // 채널 정보 설정
            channelManagerScript.channelIndex = channelList.data[0].channelIndex;
            channelManagerScript.channelName = channelList.data[0].channelName;
            channelManagerScript.cnt = channelList.data[0].cnt;
            channelManagerScript.isOnGame = channelList.data[0].isOnGame;
        }
    }

    // 빠른입장
    public void quickEnter()
    {

    }

    // 방 선택 시
    public void selectChannelElement(ChannelListElement channelListElementScript)
    {
        // 기존에 선택된 애 색깔 변경
        if(SelectedChannel != null)
        {
            SelectedChannel.transform.Find("Border").GetComponent<Image>().color = Color.white;
        }

        // 선택된 채널 설정
        SelectedChannel = channelListElementScript;
        SelectedChannel.transform.Find("Border").GetComponent<Image>().color = new Color(1f, 0.5707547f, 0.5707547f, 1f);

        //Debug.Log($"{SelectedChannel.channelIndex}, {SelectedChannel.channelName}, {SelectedChannel.cnt}, {SelectedChannel.isOnGame}");
    }

    // 방 참가 시
    public void joinChannel()
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{loginUserInfo.Name}\"," +
            "\"data\": {" +
                "\"type\":\"join\"," +
                $"\"channelIndex\":\"{SelectedChannel.channelIndex}\"" +
            "}" +
        "}";

        //Debug.Log(json);

        SendMessageToServer(json);

        // 채널 정보 설정
        channelManagerScript.channelIndex = SelectedChannel.channelIndex;
        channelManagerScript.channelName = SelectedChannel.channelName;
        channelManagerScript.cnt = SelectedChannel.cnt + 1;
        channelManagerScript.isOnGame = SelectedChannel.isOnGame;

        DataManager.Instance.channelIndex = SelectedChannel.channelIndex;
        DataManager.Instance.channelName = SelectedChannel.channelName;
        DataManager.Instance.cnt = SelectedChannel.cnt + 1;
        DataManager.Instance.isOnGame = SelectedChannel.isOnGame;

        LobbyScene.SetActive(false);
        ChannelScene.SetActive(true);
        //channelManagerScript.gameObject.SetActive(true);
    }

    [Serializable]
    class ChannelSessionList
    {
        public string type;
        public SessionData[] data;
    }
    // 채널에 참가한 세션 리스트
    public void setChannelSessionList(string response)
    {
        //Debug.Log(response);
        ChannelSessionList channelSessionList = JsonUtility.FromJson<ChannelSessionList>(response);

        channelManagerScript.sessionList.Clear();
        channelManagerScript.cnt = channelSessionList.data.Length;

        DataManager.Instance.sessionList.Clear();
        DataManager.Instance.cnt = channelSessionList.data.Length;

        foreach(SessionData d in channelSessionList.data)
        {
            channelManagerScript.sessionList.Add(d);
            DataManager.Instance.sessionList.Add(d);
        }

    }

    // 레디 버튼 클릭 시
    public void readyChannel(string json)
    {
        SendMessageToServer(json);
    }

    // 방 나가기 버튼 클릭 시
    public void leaveChannel(string json)
    {
        SendMessageToServer(json);

        clearChat();

        ChannelScene.SetActive(false);
        LobbyScene.SetActive(true);
    }

    // 게임 시작 버튼 클릭 시
    public void startGame(string json)
    {
        SendMessageToServer(json);

        //DataManager.Instance.score = new int[DataManager.Instance.cnt];
        //SceneManager.LoadScene("MainScene");
    }
    
    [Serializable]
    class SessionResponse
    {
        public string type;
        public List<SessionData> data;
    }
    // 접속한 전체 유저 목록 받아오기
    public User[] getConnectedUsers(string response)
    {
        //Debug.Log("GetAllConnectedUsers");
        //Debug.Log(response);
        SessionResponse sessionResponse = JsonUtility.FromJson<SessionResponse>(response);

        User[] userList = new User[sessionResponse.data.Count];
     
        int i = 0;
        foreach (SessionData sessinData in sessionResponse.data)
        {
            userList[i] = new User();
            userList[i].Name = sessinData.UserName;
            userList[i].UserId = sessinData.UserId;
            //Debug.Log(userList[i].Name + ", " + userList[i].UserId + ", " + response);
            i++;
        }

        return userList;
    }


    

    // ============================= 멀티 관련 =============================
    // 플레이어가 움직일 때
    public void playerMove(Vector3 position, Quaternion rotation)
    {
        string json = "{" +
            "\"channel\":\"inGame\"," +
            $"\"channelIndex\":\"{DataManager.Instance.channelIndex}\"," +
            "\"data\":{" +
                "\"type\":\"changePos\"," +
                $"\"userId\":\"{DataManager.Instance.loginUserInfo.UserId}\"," +
                $"\"x\":\"{position.x}\"," +
                $"\"y\":\"{position.y}\"," +
                $"\"z\":\"{position.z}\"," +
                $"\"rw\":\"{rotation.w}\"," +
                $"\"rx\":\"{rotation.x}\"," +
                $"\"ry\":\"{rotation.y}\"," +
                $"\"rz\":\"{rotation.z}\"" +
            "}" +
        "}";

        //Debug.Log(json);

        SendMessageToServer(json);
    }

    [Serializable]
    class PlayerPosition
    {
        public int userId;
        public float x;
        public float y;
        public float z;
        public float rw;
        public float rx;
        public float ry;
        public float rz;
    }
    // 다른 플레이어가 움직였을 때
    public void otherPlayerMove(string response)
    {
        //Debug.Log(response);
        PlayerPosition pos = JsonUtility.FromJson<PlayerPosition>(response);

        // 나의 포지션인 경우는 건너뛰기
        if(DataManager.Instance.isMe(pos.userId))
        {
            return;
        }

        for(int i = 0; i < DataManager.Instance.cnt; i++)
        {
            // 들어온 포지션의 userId의 인덱스 찾기
            if((DataManager.Instance.players != null) && (pos.userId == DataManager.Instance.sessionList[i].UserId))
            {
                Transform playerTransform = DataManager.Instance.players[i].transform;
                float interpolationFactor = 0.5f;

                Vector3 receivedPosition = new Vector3(pos.x, pos.y, pos.z);
                Quaternion receivedRotation = new Quaternion(pos.rx, pos.ry, pos.rz, pos.rw);

                // 보간
                playerTransform.position = Vector3.Lerp(playerTransform.position, receivedPosition, interpolationFactor);
                playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, receivedRotation, interpolationFactor);
                

                break;
            }
        }
    }

    // 게임 나가서 로비로 가기
    public void exitGameScene()
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{DataManager.Instance.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"leave\"," +
                $"\"channelIndex\":\"{DataManager.Instance.channelIndex}\"" +
            "}" +
        "}";

        SendMessageToServer(json);

        DataManager.Instance.players.Clear();
        DataManager.Instance.channelIndex = -1;
        DataManager.Instance.channelName = "";
        DataManager.Instance.cnt = -1;
        DataManager.Instance.clearUserId = -1;
    }

    // 게임 클리어시
    public void gameClear()
    {
        string json = "{" +
            "\"channel\":\"inGame\"," +
            $"\"channelIndex\":\"{DataManager.Instance.channelIndex}\"," +
            "\"data\":{" +
                "\"type\":\"clear\"," +
                $"\"userId\":\"{DataManager.Instance.loginUserInfo.UserId}\"" +
            "}" +
        "}";

        SendMessageToServer(json);
    }

    // 게임 누군가가 클리어 했을 때
    [Serializable]
    class ClearData
    {
        public int userId;
    }
    public void gameClearOtherPlayerSet(string response)
    {
        ClearData clearData = JsonUtility.FromJson<ClearData>(response);

        DataManager.Instance.clearUserId = clearData.userId;

        #region 씬 넘기기
        SceneManager.LoadScene("Outtro");
        ResultManager.isResultManager = true;
        #endregion
    }

    // 스코어 계산(내 점수 브로드캐스트)
    public void uploadScore()
    {
        // 내가 클리어 했으면 점수추가
        if (DataManager.Instance.clearUserId == DataManager.Instance.sessionList[DataManager.Instance.myIdx].UserId)
        {
            DataManager.Instance.score[DataManager.Instance.myIdx] += 100;
        }

        string json = "{" +
            "\"channel\":\"inGame\"," +
            $"\"channelIndex\":\"{DataManager.Instance.channelIndex}\"," +
            "\"data\":{" +
                "\"type\":\"score\"," +
                $"\"userId\":\"{DataManager.Instance.loginUserInfo.UserId}\"," +
                $"\"score\":\"{DataManager.Instance.score[DataManager.Instance.myIdx]}\"" +
            "}" +
        "}";

        Debug.Log(json);

        SendMessageToServer(json);
    }

    // 다른 사람 점수 받아서 세팅
    [Serializable]
    class Score
    {
        public int userId;
        public int score;
    }
    public void setOtherScores(string response)
    {
        // 파싱
        Score playerScore = JsonUtility.FromJson<Score>(response);

        // 해당 점수 주인 찾기
        int idx = DataManager.Instance.getUserIndex(playerScore.userId);
        // 점수 갱신
        DataManager.Instance.score[idx] = playerScore.score;
    }



    // 팔 바꾸기
    //public void playerChangeArm(string armType, string armName)
    //{
    //    string json = "{" +
    //        "\"channel\":\"inGame\"," +
    //        $"\"channelIndex\":\"{DataManager.Instance.channelIndex}\"," +
    //        "\"data\":{" +
    //            "\"type\":\"changeArm\"," +
    //            $"\"userId\":\"{DataManager.Instance.loginUserInfo.UserId}\"," +
    //            $"\"armType\":\"{armType}\"," +
    //            $"\"armName\":\"{armName}\"" +
    //        "}" +
    //    "}";

    //    Debug.Log(json);

    //    SendMessageToServer(json);
    //}

    //[Serializable]
    //class PlayerArm
    //{
    //    public int userId;
    //    public string armType;
    //    public string armName;
    //}
    // 다른사람 팔 바꾸기
    //public void otherPlayerChangeArm(string response)
    //{
    //    PlayerArm playerArm = JsonUtility.FromJson<PlayerArm>(response);

    //    Debug.Log(response);

    //    // 나의 팔인 경우는 건너뛰기
    //    if (DataManager.Instance.isMe(playerArm.userId))
    //    {
    //        Debug.Log("It's my arm!");
    //        return;
    //    }

    //    for (int i = 0; i < DataManager.Instance.cnt; i++)
    //    {
    //        // 들어온 팔의 userId의 인덱스 찾기
    //        if (playerArm.userId == DataManager.Instance.sessionList[i].UserId)
    //        {
    //            StartCoroutine(DataManager.Instance.weaponManagerScript[i].ChangeWeaponCoroutine(playerArm.armType, playerArm.armName));
    //            break;
    //        }
    //    }

        
    //}
}
