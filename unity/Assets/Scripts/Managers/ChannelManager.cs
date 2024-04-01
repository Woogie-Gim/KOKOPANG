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
    public List<SessionData> sessionList;

    private void Start()
    {
        sessionList = new List<SessionData>();
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

    // 방 내부 정보 업데이트
    public void showSessionList()
    {
        bool isAllReady = true; // 전부 레디 체크

        // 방 이름 설정
        ChannelNameText.text = channelName;

        // 유저 이름 설정
        for (int i = 0; i < PlayerNameText.Length; i++)
        {
            PlayerNameText[i].text = "";
            PlayerReadyState[i].text = "";
        }
        // 유저 레디상태 설정
        for(int i = 0; i < cnt; i++)
        {
            PlayerNameText[i].text = $"{sessionList[i].UserId}. {sessionList[i].UserName}\n";
            if(sessionList[i].IsReady)
            {
                PlayerReadyState[i].text = "Ready";
            }
            else
            {
                // 한 명이라도 레디 안했으면 isAllReady = false
                isAllReady = false;
            }
        }

        // 현재 들어온 사람 수
        JoinMemberLengthText.text = $"{cnt} / 6";

        try
        {
            // 전부 레디 시 호스트 버튼 Start로 변경, 전부 레디 아닌경우 호스트 버튼 Ready로 변경
            for (int i = 0; i < cnt; i++)
            {
                if (sessionList[i].UserId == loginManagerScript.loginUserInfo.UserId)
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

    // 레디 버튼 클릭 시
    public void readyBtnClicked()
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{loginManagerScript.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"ready\"," +
                $"\"channelIndex\":\"{channelIndex}\"" +
            "}" +
        "}";

        TCPConnectManagerScript.readyChannel(json);

        //DataManager.Instance.channelIndex = channelIndex;
        //DataManager.Instance.channelName = channelName;
        //DataManager.Instance.cnt = cnt;
        //DataManager.Instance.isOnGame = isOnGame;
        DataManager.Instance.sessionList = sessionList;

        // 클릭 표시
        ReadyBtn.GetComponent<Image>().color = Color.gray;
        ReadyBtn.GetComponent<Button>().interactable = false;
    }

    // 시작 버튼 클릭 시
    public void startBtnClicked()
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{loginManagerScript.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"start\"," +
                $"\"channelIndex\":\"{channelIndex}\"" +
            "}" +
        "}";

        // 클릭 표시
        ReadyBtn.GetComponent<Image>().color = new Color(0.373042f, 0.7830189f, 0.3924212f, 1f);
        ReadyBtn.GetComponent<Button>().interactable = true;

        TCPConnectManagerScript.startGame(json);
    }

    // 방 나가기 버튼 클릭 시
    public void quitBtnClicked() 
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{loginManagerScript.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"leave\"," +
                $"\"channelIndex\":\"{channelIndex}\"" +
            "}" +
        "}";

        // 클릭 표시
        ReadyBtn.GetComponent<Image>().color = new Color(0.373042f, 0.7830189f, 0.3924212f, 1f);
        ReadyBtn.GetComponent<Button>().interactable = true;

        TCPConnectManagerScript.leaveChannel(json);
    }

}
