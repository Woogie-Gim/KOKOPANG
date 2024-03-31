using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class CreateChannel : MonoBehaviour
{
    [Header("Commons")]
    public GameObject LobbyScene;
    public GameObject ChannelScene;
    public LoginManager loginManagerScript;
    public TCPConnectManager TCPConnectManagerScript;

    [Header("Create Channel")]
    public TMP_InputField InputRoomName;    // 방 이름 입력칸
    public TMP_InputField InputRoomPassword;    // 방 비밀번호 입력칸
    public Toggle isPrivateRoom;    // 비밀방 만들지 토글버튼



    // 비밀방 체크 확인
    public void ToggleClick()
    {
        InputRoomPassword.text = "";
        InputRoomPassword.interactable = isPrivateRoom.isOn;
    }

    // 방 만들기 버튼 클릭 시
    public void CreateChannelBtnClicked()
    {
        string json = "{" +
            "\"channel\":\"channel\"," +
            $"\"userName\":\"{loginManagerScript.loginUserInfo.Name}\"," +
            "\"data\":{" +
                "\"type\":\"create\"," +
                $"\"channelName\":\"{InputRoomName.text}\"" +
            "}" +
        "}";
        //Debug.Log(json);

        TCPConnectManagerScript.createChannel(json);
    }

    // 방 만들기 닫기 버튼 클릭 시
    public void CloseBtnClicked()
    {
        InputRoomName.text = "";
        InputRoomPassword.text = "";
        gameObject.SetActive(false);
    }
}
