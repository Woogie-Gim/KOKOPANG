using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomListElement : MonoBehaviour
{

    private int channelIndex;
    private string channelName;
    private int cnt;
    private bool isOnGame;

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


}
