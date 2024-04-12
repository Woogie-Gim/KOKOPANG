using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChannelManager : MonoBehaviour
{
    [Header("Commons")]
    public GameObject LobbyScene;
    public GameObject ChannelScene;
    public LoginManager loginManagerScript;
    public TCPConnectManager TCPConnectManagerScript;

    [Header("Channel")]
    public TMP_Text ChannelNameText;
    public TMP_Text[] PlayerNameText;
    public TMP_Text[] PlayerReadyState;
    public TMP_Text JoinMemberLengthText;
    public GameObject ReadyBtn;
    public GameObject StartBtn;

    public int channelIndex;
    public string channelName;
    public int cnt;
    public bool isOnGame;
    public List<SessionData> sessionList = new List<SessionData>();

    private void Start()
    {
        //TCPConnectManagerScript = GameObject.Find("TCPConnectManager").GetComponent<TCPConnectManager>();
        //sessionList = 
    }

    void OnEnable()
    {
        //ChannelNameText.text = channelName;

        //foreach (TMP_Text t in PlayerNameText)
        //{
        //    t.text = "";
        //}
        //showSessionList();

        //JoinMemberLengthText.text = cnt.ToString();
    }

    private void OnDisable()
    {
        
    }

    // �� ���� ���� ������Ʈ
    public void showSessionList()
    {
        bool isAllReady = true; // ���� ���� üũ

        // �� �̸� ����
        ChannelNameText.text = channelName;

        // �� �ʱ�ȭ
        for (int i = 0; i < PlayerNameText.Length; i++)
        {
            PlayerNameText[i].text = "";
            PlayerReadyState[i].text = "";
        }
        for(int i = 0; i < cnt; i++)
        {
            // ���� �̸� ����
            PlayerNameText[i].text = $"{sessionList[i].UserId}. {sessionList[i].UserName}";

            if(sessionList[i].UserId == DataManager.Instance.loginUserInfo.UserId)
            {
                PlayerNameText[i].text += " (��)";
            }
            
            // ���� ������� ����
            if(sessionList[i].IsReady)
            {
                PlayerReadyState[i].text = "Ready";
            }
            else
            {
                // �� ���̶� ���� �������� isAllReady = false
                isAllReady = false;
            }
        }

        // ���� ���� ��� ��
        JoinMemberLengthText.text = $"{cnt} / 6";

        try
        {
            // ���� ���� �� ȣ��Ʈ ��ư Start�� ����, ���� ���� �ƴѰ�� ȣ��Ʈ ��ư Ready�� ����
            for (int i = 0; i < cnt; i++)
            {
                if (sessionList[i].UserId == DataManager.Instance.loginUserInfo.UserId)   // chk
                {
                    if (sessionList[i].IsHost)
                    {
                        ReadyBtn.SetActive(!isAllReady);
                        StartBtn.SetActive(isAllReady);
                    }
                    break;
                }
            }
        }
        catch(Exception e)
        {
            return;
        }
        

    }

    // ���� ��ư Ŭ�� ��
    public void readyBtnClicked()
    {
        // chk
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{DataManager.Instance.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"ready\"," +
                $"\"channelIndex\":\"{channelIndex}\"" +
            "}" +
        "}";

        TCPConnectManager.Instance.readyChannel(json);  // chk

        //DataManager.Instance.channelIndex = channelIndex;
        //DataManager.Instance.channelName = channelName;
        //DataManager.Instance.cnt = cnt;
        //DataManager.Instance.isOnGame = isOnGame;
        //DataManager.Instance.sessionList.Clear();
        //DataManager.Instance.sessionList = sessionList;

        // Ŭ�� ǥ��
        ReadyBtn.GetComponent<Image>().color = Color.gray;
        ReadyBtn.GetComponent<Button>().interactable = false;
    }

    // ���� ��ư Ŭ�� ��
    public void startBtnClicked()
    {
        // chk
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{DataManager.Instance.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"start\"," +
                $"\"channelIndex\":\"{channelIndex}\"" +
            "}" +
        "}";

        // Ŭ�� ǥ��
        ReadyBtn.GetComponent<Image>().color = new Color(0.373042f, 0.7830189f, 0.3924212f, 1f);
        ReadyBtn.GetComponent<Button>().interactable = true;

        TCPConnectManager.Instance.startGame(json); // chk
    }

    // �� ������ ��ư Ŭ�� ��
    public void quitBtnClicked() 
    {
        // chk
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{DataManager.Instance.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"leave\"," +
                $"\"channelIndex\":\"{channelIndex}\"" +
            "}" +
        "}";

        // Ŭ�� ǥ��
        ReadyBtn.GetComponent<Image>().color = new Color(0.373042f, 0.7830189f, 0.3924212f, 1f);
        ReadyBtn.GetComponent<Button>().interactable = true;

        sessionList.Clear();
        DataManager.Instance.sessionList.Clear();
        DataManager.Instance.score = null;

        TCPConnectManager.Instance.leaveChannel(json);  // chk
    }

}
