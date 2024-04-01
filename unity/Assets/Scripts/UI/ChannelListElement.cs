using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChannelListElement : MonoBehaviour
{
    public TCPConnectManager TCPConnectManagerScript;

    public TMP_Text channelText;
    public Button channelBtn;

    public int channelIndex;
    public string channelName;
    public int cnt;
    public bool isOnGame;

    public int ChannelIndex
    {
        get
        {
            return channelIndex;
        }
        set
        {
            channelIndex = value;
        }
    }

    public string ChannelName
    {
        get
        {
            return channelName;
        }
        set
        {
            channelName = value;
        }
    }

    public int Cnt
    {
        get
        {
            return cnt;
        }
        set
        {
            cnt = value;
        }
    }

    public bool IsOnGame
    {
        get
        {
            return isOnGame;
        }
        set
        {
            isOnGame = value;
        }
    }

    private void Start()
    {
        TCPConnectManagerScript = GameObject.Find("TCPConnectManager").GetComponent<TCPConnectManager>();

        channelText.text = $"{channelIndex}. {channelName}    ({cnt}/6)";
    }

    // 방 리스트에서 버튼 누르면
    public void ChannelListElementClicked()
    {
        TCPConnectManagerScript.selectChannelElement(this);
    }
}
