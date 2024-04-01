using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [Header("LoginUserInfo")]
    public string accessToken; // access token
    public string refreshToken;    // refresh token
    public User loginUserInfo;

    [Header("ChannelInfo")]
    public int channelIndex;
    public string channelName;
    public int cnt;
    public bool isOnGame;
    public List<SessionData> sessionList;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
