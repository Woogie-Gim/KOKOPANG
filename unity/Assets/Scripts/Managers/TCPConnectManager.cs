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
    [Header("Commons")]
    public LobbyManager lobbyManagerScript;
    public LoginManager loginManagerScript;

    [Header("Chat")]
    public TMP_Text MessageElement;   // ä�� �޽���
    public GameObject ChattingList; // ä�� ����Ʈ
    public TMP_InputField InputText;  // �Է� �޽���


    private TcpClient _tcpClient;
    private NetworkStream _networkStream;
    private StreamReader reader;
    private StreamWriter writer;
    private User loginUserInfo;

    private string hostname = "j10c211.p.ssafy.io";
    private int port = 1370;

    private void Awake()
    {
        ConnectToServer();
    }

    private void Update()
    {
        // �����Ͱ� ���� ���
        if(_networkStream.DataAvailable)
        {
            string response = ReadMessageFromServer();
            string type = getType(response);

            //Debug.Log(response);
            //Debug.Log(type);
            
            if(type == "chat")  // ä�� �޽���
            {
                showMessage(response);
            }
            else if(type == "channelList")  // ��ü ������ �� ���
            {

            }
            else if(type == "sessionList")  // ��ü ������ ���� ���
            {
                lobbyManagerScript.setAllUsers(response);
            }
            else if(type == "channelSessionList")   // �� ���� ���� ���
            {

            }
        }

        // ä�� �Է� ����
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if(InputText.text != "")
            {
                MessageSendBtnClicked();
            }
        }
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

            loginUserInfo = loginManagerScript.loginUserInfo;
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


            //string response = ReadMessageFromServer();
            //Debug.Log(response);
        }
        catch (Exception e)
        {
            // ���� �� ���� �߻� ��
            Debug.Log($"Failed to connect to the server: {e.Message}");
        }
    }

    // response ���� �޽��� Ÿ�� üũ�ϱ�
    private string getType(string response)
    {
        string[] words = response.Split('\"');
        return words[3];
    }

    // ������ �޽��� ������
    public void SendMessageToServer(string message)
    {
        if (_tcpClient == null)
        {
            return;
        }
        writer.WriteLine(message);
        writer.Flush(); // �޽��� ��� ����
    }

    // �������� �޽��� �б�
    public string ReadMessageFromServer()
    {
        if(_tcpClient == null)
        {
            return "";
        }

        try
        {
            // �����κ��� ���� �б�
            string response = reader.ReadLine();
            return response;
        }
        catch(Exception e)
        {
            Debug.Log("���� �б� ����: " + e.Message);
            return "";
        }
    }

    // ============================= ä�� ���� =============================
    // �޽��� ���� ��ư Ŭ�� ��
    public void MessageSendBtnClicked()
    {
        string message = InputText.text;

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

        InputText.text = "";

        SendMessageToServer(json);
        InputText.Select();
        InputText.ActivateInputField();
    }


    // �޽��� ������ ��
    // TODO: �޽��� ������Ʈ Ǯ�� �����ϱ�
    public void showMessage(string message)
    {
        // ���� �θ� ������Ʈ
        Transform content = ChattingList.transform.Find("Viewport/Content");

        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        TMP_Text temp1 = Instantiate(MessageElement);
        temp1.text = chatMessage.UserName + ": " + chatMessage.Message;
        temp1.transform.SetParent(content, false);

        // 20�� �Ѿ�� ä�� ���������� �����
        // TODO: ������Ʈ Ǯ��
        if(content.childCount >= 20)
        {
            Destroy(content.GetChild(1).gameObject);
        }

        StartCoroutine(ScrollToBottom());
    }

    // ��ũ�� �� �Ʒ��� ������
    IEnumerator ScrollToBottom()
    {
        // ���� ������ ��ٸ�
        yield return null;

        Transform content = ChattingList.transform.Find("Viewport/Content");

        // Layout Group�� ������ ��� ������Ʈ
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)content);

        // ��ũ�� �� �Ʒ��� ����
        ChattingList.GetComponent<ScrollRect>().verticalNormalizedPosition = 0f;
    }

    // ============================= ��(channel), ����(session) ���� =============================
    // �� ����Ʈ �ҷ�����
    public void setChannelList()
    {

    }

    // TODO: �� �����
    public void createChannel(string roomName)
    {
        string json = "{" +
            "\"channel\":\"room\"," +
            $"\"userName\":\"{loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"enter\"," +
                $"\"channelName\":\"{roomName}\"" +
            "}" +
        "}";

        SendMessageToServer(json);
    }

    // ��������
    public void quickEnter()
    {

    }

    // �� ����
    public void participate()
    {

    }
    
    [Serializable]
    class SessionResponse
    {
        public string type;
        public List<SessionData> data;
    }
    [Serializable]
    class SessionData
    {
        public string userName;
        public int userId;
    }
    // ������ ��ü ���� ��� �޾ƿ���
    public User[] getConnectedUsers(string response)
    {
        //Debug.Log(response);
        SessionResponse sessionResponse = JsonUtility.FromJson<SessionResponse>(response);

        User[] userList = new User[sessionResponse.data.Count];
     
        int i = 0;
        foreach (SessionData sessinData in sessionResponse.data)
        {
            userList[i] = new User();
            userList[i].Name = sessinData.userName;
            userList[i].UserId = sessinData.userId;
            Debug.Log(userList[i].Name + ", " + userList[i].UserId + ", " + response);
            i++;
        }

        return userList;
    }


    // ============================= ���� ���� =============================
    // ���� ��
    private void OnApplicationQuit()
    {
        if (_tcpClient != null)
        {
            DisconnectFromServer();
        }
    }

    public void DisconnectFromServer()
    {
        // ���� ����
        _networkStream.Close();
        _tcpClient.Close();
    }
}
