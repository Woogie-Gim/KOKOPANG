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
    //public GameObject IngameChatUI;   // �ΰ��� ä��
    public TMP_Text MessageElement;   // ä�� �޽���
    public GameObject LobbyChattingList; // �κ� ä�� ����Ʈ
    public GameObject ChannelChattingList; // ä�� ä�� ����Ʈ
    public GameObject InGameChattingList; // �ΰ��� ä�� content
    public TMP_InputField LobbyChat;  // �κ� �Է� �޽���
    public TMP_InputField ChannelChat;  // ä�� �Է� �޽���
    public TMP_InputField InGameChat;  // �ΰ��� ä�� InputText
    public Button LobbyChatSendBtn;     // �κ� �޽��� ���۹�ư
    public Button ChannelChatSendBtn;   // ä�� �޽��� ���� ��ư
    private bool isInGameChatUICloseRunning = false;


    [Header("Channel")]
    public GameObject ScrollViewChannelList;    // ä�� ����Ʈ
    public GameObject ChannelListElement;       // ä�� ����Ʈ �׸�
    public ChannelListElement SelectedChannel;  // ���õ� ä�� ����Ʈ



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
        // �̱���
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���� ������ instance�� ����
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
            //Instance.IngameChatUI = IngameChatUI;
            Instance.InGameChattingList = InGameChattingList;
            Instance.InGameChat = InGameChat;
            Instance.ScrollViewChannelList = ScrollViewChannelList;
            Instance.ChannelListElement = ChannelListElement;
            Destroy(gameObject);
        }

        // ������ ���̱�
        Instance.LobbyChatSendBtn.onClick.RemoveAllListeners();
        Instance.ChannelChatSendBtn.onClick.RemoveAllListeners();
        Instance.LobbyChatSendBtn.onClick.AddListener(() => Instance.MessageSendBtnClicked(LobbyChat));
        Instance.ChannelChatSendBtn.onClick.AddListener(() => Instance.MessageSendBtnClicked(ChannelChat));

        // ���� �α��� �����Ͱ� �ִٸ� �κ�Ŵ��� �ѱ�
        if(DataManager.Instance.loginUserInfo.UserId != 0)
        {
            lobbyManagerScript.gameObject.SetActive(true);
        }
    }

    //private void OnEnable()
    //{
    //    // TCP ù ���� �Ŀ��� ��û 2�� �޾Ƽ� channel, session 2���� ����� �޾ƿ´�.
    //    ConnectToServer();
    //}

    private void OnDisable()
    {
        // ���Ӱ��� �޸� ����
        OnApplicationQuit();
    }

    // ============================= ���� ���� =============================
    // ���� ��
    public void OnApplicationQuit()
    {
        ResultManager.isResultManager = false;
        // ä�ó��� �����
        clearChat();

        DataManager.Instance.gameDataClear();

        // tcp���� ����
        if (_tcpClient != null)
        {
            DisconnectFromServer();
        }
        writer = null;
        loginUserInfo = null;
    }

    public void DisconnectFromServer()
    {
        // ���� ����
        _networkStream.Close();
        _tcpClient.Close();
        _networkStream = null;
        _tcpClient = null;
    }

    private void Update()
    {
        // �����Ͱ� ���� ���
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

        // ä�� �Է� ����
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // �α��� ������ ����
            if(SceneManager.GetActiveScene().name == "Login")
            {
                // �κ��� ���
                if(LobbyScene.activeSelf)
                {
                    MessageSendBtnClicked(LobbyChat);
                }
                // ä���� ���
                else if(ChannelScene.activeSelf)
                {
                    MessageSendBtnClicked(ChannelChat);
                }
            }
            // �ΰ��ӿ��� ����
            else if(SceneManager.GetActiveScene().name == "MainScene")
            {
                // ä�� �Է�â ����������
                if(!InGameChat.gameObject.activeSelf)
                {
                    // ä�� �Է�â �Ѱ� ��Ŀ�� �ű��
                    InGameChat.gameObject.SetActive(true);
                    InGameChattingList.SetActive(true);
                    InGameChat.Select();
                    InGameChat.ActivateInputField();
                }
                // ä�� �Է�â ����������
                else if(InGameChat.gameObject.activeSelf)
                {
                    // ä�� �Է� �ƹ��͵� ������ ����
                    if(InGameChat.text == "")
                    {
                        InGameChat.gameObject.SetActive(false);
                        if(!isInGameChatUICloseRunning)
                        {
                            StartCoroutine(closeInGameChat(5));
                        }
                    }
                    // �ԷµȰ� ������ ä�� ������
                    else
                    {
                        MessageSendBtnClicked(InGameChat);
                    }
                }
            }
        }
    }

    // 5�� �� ä�� ����Ʈ �ݱ�
    IEnumerator closeInGameChat(float time)
    {
        isInGameChatUICloseRunning = true;

        yield return new WaitForSeconds(time);

        InGameChattingList.SetActive(false);

        isInGameChatUICloseRunning = false;
    }

    // ��û �й��ϱ�
    private void DispatchResponse(string response)
    {
        string type = getType(response);

        //Debug.Log("response: " + response);
        //Debug.Log("type: " + type);

        if (type == "chat")  // ä�� �޽���
        {
            Debug.Log(response);
            if(SceneManager.GetActiveScene().name == "Login")
            {
                if(LobbyScene.activeSelf)
                {
                    showMessage(response, LobbyChattingList);
                }
                else if(ChannelScene.activeSelf)
                {
                    showMessage(response, ChannelChattingList);
                }
            }
            else if(SceneManager.GetActiveScene().name == "MainScene")
            {
                showMessage(response, InGameChattingList);
            }
        }
        else if (type == "channelList")  // ��ü ������ �� ���
        {
            setChannelList(response);
        }
        else if (type == "sessionList")  // ��ü ������ ���� ���
        {
            lobbyManagerScript.setAllUsers(response);
        }
        else if (type == "channelSessionList")   // �� ���� ���� ���
        {
            setChannelSessionList(response);
            channelManagerScript.showSessionList();
        }
        else if(type == "channelInfo")  // ä�� ����
        {
            setChannelInfo(response);
            channelManagerScript.showSessionList();
        }
        else if (type == "changePos")    // �ٸ���� ������ ���� ��
        {
            otherPlayerMove(response);
        }
        else if(type == "score") // ���� �ٸ���� ���� �����͸Ŵ����� ����
        {
            setOtherScores(response);
        }
        else if(type == "clear")    // �������� ���� Ŭ���� ��
        {
            gameClearOtherPlayerSet(response);
        }
        //else if(type == "changeArm")    // �� ���� ��
        //{
        //    otherPlayerChangeArm(response);
        //}
        else
        {
            Debug.Log("Response ELSE!!!: " + response);
        }
    }

    // response ���� �޽��� Ÿ�� üũ�ϱ�
    private string getType(string response)
    {
        string[] words = response.Split('\"');
        return words[3];
    }

    // ============================= ���� ���� ���� =============================
    public void ConnectToServer()
    {
        try
        {
            // TCP ������ ����
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

            // channel, session ��� �޾ƿ���
            // TCP ù ���� �Ŀ��� ��û 2�� �޾Ƽ� channel, session 2���� ����� �޾ƿ´�.
            //string response = ReadMessageFromServer();
            //DispatchResponse(response);

            //string response = ReadMessageFromServer();
            //Debug.Log(response);
        }
        catch (Exception e)
        {
            // ���� �� ���� �߻� ��
            Debug.Log($"Failed to connect to the server: {e.Message}");
        }
    }

    // ������ �޽��� ������
    private void SendMessageToServer(string message)
    {
        if (_tcpClient == null)
        {
            return;
        }
        //Debug.Log(message);
        writer.WriteLine(message);
        writer.Flush(); // �޽��� ��� ����
    }

    // �������� �޽��� �б�
    private string ReadMessageFromServer()
    {
        if(_tcpClient == null)
        {
            return null;
        }

        try
        {
            // �����κ��� ���� �б�
            //string response = reader.ReadLine();
            //return response;

            //StringBuilder message = new StringBuilder();
            //while (_networkStream.CanRead)
            //{
            //    int readByte = _networkStream.ReadByte();
            //    if (readByte == -1 || readByte == '\n') // '\n'�� �����ڷ� ���
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
            //        if (readChar == '\n') // '\n'�� �����ڷ� ���
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
                if (readChar == '\n') // '\n'�� �����ڷ� ���
                {
                    break;
                }
                message.Append(readChar);
            }
            return message.ToString();
        }
        catch(Exception e)
        {
            Debug.Log("���� �б� ����: " + e.Message);
            return null;
        }
    }

    // ============================= ä�� ���� =============================
    // �޽��� ���� ��ư Ŭ�� ��
    public void MessageSendBtnClicked(TMP_InputField inputField)
    {
        Debug.Log("�޽��� ����");

        string message = inputField.text;

        if (message == "")
        {
            return;
        }

        string json;
        // �κ� ä���� ���
        if (SceneManager.GetActiveScene().name == "Login" && LobbyScene.activeSelf)
        {
            json = "{" +
                "\"channel\":\"lobby\"," +
                $"\"userName\":\"{loginUserInfo.Name}\"," +
                "\"data\":{" +
                    "\"type\":\"chat\"," +
                    "\"message\":" + "\"" + message + "\"" +
                "}" +
            "}";
        }
        // ä�� ä���� ���
        else
        {
            json = "{" +
                "\"channel\":\"channel\"," +
                $"\"userName\":\"{DataManager.Instance.loginUserInfo.Name}\"," +
                "\"data\":{" +
                    "\"type\":\"chat\"," +
                    $"\"channelIndex\":\"{DataManager.Instance.channelIndex}\"," +
                    "\"message\":" + "\"" + message + "\"" +
                "}" +
            "}";
        }

        inputField.text = "";
        SendMessageToServer(json);
        inputField.Select();
        inputField.ActivateInputField();
    }


    // �޽��� ������ ��
    // TODO: �޽��� ������Ʈ Ǯ�� �����ϱ�
    public void showMessage(string message, GameObject ChatScrollView)
    {
        InGameChattingList.SetActive(true);
        // ���� �θ� ������Ʈ
        Transform content = ChatScrollView.transform.Find("Viewport/Content");

        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        // ���� ���� �޽��� ǥ���ϱ�
        string me = "";
        if (chatMessage.UserName == DataManager.Instance.loginUserInfo.Name)
        {
            me = "(��)";
        }

        TMP_Text temp1 = Instantiate(MessageElement);
        //temp1.text = chatMessage.UserName + ": " + chatMessage.Message;
        temp1.text = $"{chatMessage.UserName} {me} : {chatMessage.Message}";
        temp1.transform.SetParent(content, false);



        // 20�� �Ѿ�� ä�� ���������� �����
        // TODO: ������Ʈ Ǯ��
        if (content.childCount >= 20)
        {
            Destroy(content.GetChild(1).gameObject);
        }

        StartCoroutine(ScrollToBottom(ChatScrollView));
        if(!isInGameChatUICloseRunning)
        {
            StartCoroutine(closeInGameChat(5));
        }
    }

    // ��ũ�� �� �Ʒ��� ������
    IEnumerator ScrollToBottom(GameObject ChatScrollView)
    {
        // ���� ������ ��ٸ�
        yield return null;

        Transform content = ChatScrollView.transform.Find("Viewport/Content");

        // Layout Group�� ������ ��� ������Ʈ
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);

        // ��ũ�� �� �Ʒ��� ����
        ChatScrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    // ä�ó��� �����
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

    // ============================= ��(channel), ����(session) ���� =============================
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
    // �� ����Ʈ �ҷ�����
    public void setChannelList(string response)
    {
        //Debug.Log("���� setChannelList");
        // ä�� ����Ʈ �� ��ũ�Ѻ�(������ �θ�)
        Transform content = ScrollViewChannelList.transform.Find("Viewport/Content");
        // ���� ����Ʈ ����
        // TODO: ������Ʈ Ǯ��
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // JSON �Ľ�
        ChannelList channelList = JsonUtility.FromJson<ChannelList>(response);

        for(int i = 0; i < channelList.data.Length; i++)
        {
            // ���� �ο� ������ �Ⱥ��̰� �ϱ�
            if(channelList.data[i].isOnGame || channelList.data[i].cnt == 6)
            {
                continue;
            }

            //Debug.Log("���̸�: " + channelList.data[i].channelName);
            // ������ �����
            GameObject channelListElement = Instantiate(ChannelListElement);
            ChannelListElement channelListElementScript = channelListElement.GetComponent<ChannelListElement>();
            channelListElementScript.ChannelIndex = channelList.data[i].channelIndex;
            channelListElementScript.ChannelName = channelList.data[i].channelName;
            channelListElementScript.Cnt = channelList.data[i].cnt;
            channelListElementScript.IsOnGame = channelList.data[i].isOnGame;

            // �θ� ���̱�
            channelListElement.transform.SetParent(content, false);
        }
    }

    // �� �����
    public void createChannel(string json)
    {
        //Debug.Log("ä�� �����: " + json);
        SendMessageToServer(json);

        // ������ �޾ƿ���
        //string response = ReadMessageFromServer();
        //DispatchResponse(response);

        LobbyScene.SetActive(false);
        ChannelScene.SetActive(true);

        //channelManagerScript.gameObject.SetActive(true);
    }

    // �� ���� ����
    public void setChannelInfo(string response)
    {
        // JSON �Ľ�
        ChannelList channelList = JsonUtility.FromJson<ChannelList>(response);

        DataManager.Instance.channelIndex = channelList.data[0].channelIndex;
        DataManager.Instance.channelName = channelList.data[0].channelName;
        DataManager.Instance.cnt = channelList.data[0].cnt;
        DataManager.Instance.isOnGame = channelList.data[0].isOnGame;

        // ���ӽ���
        if (channelList.data[0].isOnGame)
        {
            if(SceneManager.GetActiveScene().name == "Login")
            {
                DataManager.Instance.score = new int[DataManager.Instance.cnt];
                SceneManager.LoadScene("Intro");
            }
        }
        // ä�� ������(���ӽ���x)
        else
        {
            Debug.Log(channelList.data[0].channelName);
            // ä�� ���� ����
            channelManagerScript.channelIndex = channelList.data[0].channelIndex;
            channelManagerScript.channelName = channelList.data[0].channelName;
            channelManagerScript.cnt = channelList.data[0].cnt;
            channelManagerScript.isOnGame = channelList.data[0].isOnGame;
        }
    }

    // ��������
    public void quickEnter()
    {

    }

    // �� ���� ��
    public void selectChannelElement(ChannelListElement channelListElementScript)
    {
        // ������ ���õ� �� ���� ����
        if(SelectedChannel != null)
        {
            SelectedChannel.transform.Find("Border").GetComponent<Image>().color = Color.white;
        }

        // ���õ� ä�� ����
        SelectedChannel = channelListElementScript;
        SelectedChannel.transform.Find("Border").GetComponent<Image>().color = new Color(1f, 0.5707547f, 0.5707547f, 1f);

        //Debug.Log($"{SelectedChannel.channelIndex}, {SelectedChannel.channelName}, {SelectedChannel.cnt}, {SelectedChannel.isOnGame}");
    }

    // �� ���� ��
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

        // ä�� ���� ����
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
        SelectedChannel = null;
        //channelManagerScript.gameObject.SetActive(true);
    }

    [Serializable]
    class ChannelSessionList
    {
        public string type;
        public SessionData[] data;
    }
    // ä�ο� ������ ���� ����Ʈ
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

    // ���� ��ư Ŭ�� ��
    public void readyChannel(string json)
    {
        SendMessageToServer(json);
    }

    // �� ������ ��ư Ŭ�� ��
    public void leaveChannel(string json)
    {
        SendMessageToServer(json);

        clearChat();

        ChannelScene.SetActive(false);
        LobbyScene.SetActive(true);
    }

    // ���� ���� ��ư Ŭ�� ��
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
    // ������ ��ü ���� ��� �޾ƿ���
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


    

    // ============================= ��Ƽ ���� =============================
    // �÷��̾ ������ ��
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
    // �ٸ� �÷��̾ �������� ��
    public void otherPlayerMove(string response)
    {
        //Debug.Log(response);
        PlayerPosition pos = JsonUtility.FromJson<PlayerPosition>(response);

        // ���� �������� ���� �ǳʶٱ�
        if(DataManager.Instance.isMe(pos.userId))
        {
            return;
        }

        for(int i = 0; i < DataManager.Instance.players.Count; i++)
        {
            // ���� �������� userId�� �ε��� ã��
            if((DataManager.Instance.players != null) && (pos.userId == DataManager.Instance.sessionList[i].UserId))
            {
                Transform playerTransform = DataManager.Instance.players[i].transform;
                float interpolationFactor = 0.5f;

                Vector3 receivedPosition = new Vector3(pos.x, pos.y, pos.z);
                Quaternion receivedRotation = new Quaternion(pos.rx, pos.ry, pos.rz, pos.rw);

                // ����
                playerTransform.position = Vector3.Lerp(playerTransform.position, receivedPosition, interpolationFactor);
                playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, receivedRotation, interpolationFactor);
                

                break;
            }
        }
    }

    // ���� ������ �κ�� ����
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

    // ���� Ŭ�����
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

    // ���� �������� Ŭ���� ���� ��
    [Serializable]
    class ClearData
    {
        public int userId;
    }
    public void gameClearOtherPlayerSet(string response)
    {
        ClearData clearData = JsonUtility.FromJson<ClearData>(response);

        DataManager.Instance.clearUserId = clearData.userId;

        #region �� �ѱ��
        SceneManager.LoadScene("Outtro");
        ResultManager.isResultManager = true;
        #endregion
    }

    // ���ھ� ���(�� ���� ��ε�ĳ��Ʈ)
    public void uploadScore()
    {
        // ���� Ŭ���� ������ �����߰�
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

    // �ٸ� ��� ���� �޾Ƽ� ����
    [Serializable]
    class Score
    {
        public int userId;
        public int score;
    }
    public void setOtherScores(string response)
    {
        // �Ľ�
        Score playerScore = JsonUtility.FromJson<Score>(response);

        // �ش� ���� ���� ã��
        int idx = DataManager.Instance.getUserIndex(playerScore.userId);
        // ���� ����
        DataManager.Instance.score[idx] = playerScore.score;
    }



    // �� �ٲٱ�
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
    // �ٸ���� �� �ٲٱ�
    //public void otherPlayerChangeArm(string response)
    //{
    //    PlayerArm playerArm = JsonUtility.FromJson<PlayerArm>(response);

    //    Debug.Log(response);

    //    // ���� ���� ���� �ǳʶٱ�
    //    if (DataManager.Instance.isMe(playerArm.userId))
    //    {
    //        Debug.Log("It's my arm!");
    //        return;
    //    }

    //    for (int i = 0; i < DataManager.Instance.cnt; i++)
    //    {
    //        // ���� ���� userId�� �ε��� ã��
    //        if (playerArm.userId == DataManager.Instance.sessionList[i].UserId)
    //        {
    //            StartCoroutine(DataManager.Instance.weaponManagerScript[i].ChangeWeaponCoroutine(playerArm.armType, playerArm.armName));
    //            break;
    //        }
    //    }

        
    //}
}
