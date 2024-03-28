using System;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TCPConnectManager : MonoBehaviour
{
    [Header("Chat")]
    public TMP_Text MessageElement;   // 채팅 메시지
    public GameObject ChattingList; // 채팅 리스트
    public TMP_InputField InputText;  // 입력 메시지


    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private StreamReader reader;
    private StreamWriter writer;

    private string hostname = "192.168.100.82";
    private int port = 1370;

    private void Start()
    {
        ConnectToServer();
    }

    private void Update()
    {
        // 데이터가 들어온 경우
        if(_networkStream.DataAvailable)
        {
            string response = ReadMessageFromServer();
            string type = getType(response);
            
            if(type == "chat")  // 채팅 메시지
            {
                showMessage(response);
            }
            else if(type == "channelList")  // 전체 생성된 방 목록
            {

            }
            else if(type == "sessionList")  // 전체 접속한 유저 목록
            {

            }
            else if(type == "channelSessionList")   // 방 안의 유저 목록
            {

            }
        }

        // 채팅 입력 엔터
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(InputText.text != "")
            {
                MessageSendBtnClicked();
            }
        }
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

            string json =   "{" +
                "\"channel\":\"lobby\"," +
                "\"userName\":\"010101\"," +
                "\"userId\":0" +
            "}";

            
            SendMessageToServer(json);

            string response = ReadMessageFromServer();
            Debug.Log(response);
        }
        catch (Exception e)
        {
            // 연결 중 오류 발생 시
            Debug.Log($"Failed to connect to the server: {e.Message}");
        }
    }

    // response 받은 메시지 타입 체크하기
    private string getType(string response)
    {
        string[] words = response.Split('\"');
        return words[3];
    }

    // 서버로 메시지 보내기
    public void SendMessageToServer(string message)
    {
        if (_tcpClient == null)
        {
            return;
        }
        writer.WriteLine(message);
        writer.Flush(); // 메시지 즉시 전송
    }

    // 서버에서 메시지 읽기
    public string ReadMessageFromServer()
    {
        if(_tcpClient == null)
        {
            return "";
        }

        try
        {
            // 서버로부터 응답 읽기
            string response = reader.ReadLine();
            return response;
        }
        catch(Exception e)
        {
            Debug.Log("응답 읽기 실패: " + e.Message);
            return "";
        }
    }

    // ============================= 채팅 관련 =============================
    // 메시지 전송 버튼 클릭 시
    public void MessageSendBtnClicked()
    {
        string message = InputText.text;

        string json = "{" +
            "\"channel\":\"lobby\"," +
            "\"userName\":\"010101\"," +
            "\"data\":{" +
                "\"type\":\"chat\"," +
                "\"message\":" + "\"" + message + "\"" +
            "}" +
        "}";

        InputText.text = "";

        SendMessageToServer(json);
        InputText.Select();
        InputText.ActivateInputField();
    }


    // 메시지 들어왔을 때
    // TODO: 메시지 오브젝트 풀링 적용하기
    public void showMessage(string message)
    {
        // 붙일 부모 오브젝트
        Transform content = ChattingList.transform.Find("Viewport/Content");

        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        TMP_Text temp1 = Instantiate(MessageElement);
        temp1.text = chatMessage.UserName + ": " + chatMessage.Message;
        temp1.transform.SetParent(content, false);

        StartCoroutine(ScrollToBottom());
    }

    // 스크롤 맨 아래로 내리기
    IEnumerator ScrollToBottom()
    {
        // 다음 프레임 기다림
        yield return null;

        Transform content = ChattingList.transform.Find("Viewport/Content");

        // Layout Group을 강제로 즉시 업데이트
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);

        // 스크롤 맨 아래로 내림
        ChattingList.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    // ============================= 방(channel), 유저(session) 관련 =============================
    // 방 리스트 불러오기
    public void setChannelList()
    {

    }

    // 방 만들기
    public void createChannel()
    {

    }

    // 빠른입장
    public void quickEnter()
    {

    }

    // 방 참가
    public void participate()
    {

    }
    
    // 접속한 전체 유저 목록 받아오기
    public string getConnectedUsers()
    {
        

        return "";
    }


    // ============================= 종료 관련 =============================
    // 종료 시
    private void OnApplicationQuit()
    {
        if (_tcpClient != null)
        {
            DisconnectFromServer();
        }
    }

    public void DisconnectFromServer()
    {
        // 연결 종료
        _networkStream.Close();
        _tcpClient.Close();
    }
}
