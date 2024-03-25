using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserListElement : MonoBehaviour
{
    public GameObject UserInfoDetail;

    public LobbyManager lobbyManagerScript;

    public TMP_Text userNameText;
    public Button detailBtn;

    private int id;
    private string email;
    private new string name;


    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }
        set
        {
            email = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }


    private void Start()
    {
        // 이벤트 붙이기
        detailBtn.onClick.AddListener(clickOpenUserDetailBtn);

        lobbyManagerScript = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
    }

    // 유저 상세 페이지 열기 버튼 클릭 시
    public void clickOpenUserDetailBtn()
    {
        UserInfoDetail script = UserInfoDetail.GetComponent<UserInfoDetail>();
        UserInfoDetail.SetActive(true);

        script.Email = Email;
        script.Name = Name;
        script.Id = Id;

        script.detailUserNameText.text = Name;
        script.detailEmailText.text = Email;

        // 친구인지 확인 후 친구추가 버튼 바꾸기
        StartCoroutine(setFriendStatus());
    }

    // 친구 상태별 버튼 바꾸기
    private IEnumerator setFriendStatus()
    {
        string result = "";
        yield return StartCoroutine(lobbyManagerScript.FriendCheckRequest(Id, value => result = value));

        UserInfoDetail userInfoDetailScript = UserInfoDetail.GetComponent<UserInfoDetail>();

        // 현재 친구
        if (result == "friend")
        {
            Debug.Log("friend");
            userInfoDetailScript.addFriendBtn.interactable = false;
            userInfoDetailScript.addFriendText.text = "친구";
        }
        // 내가 요청 보내서 수락 받기 대기중
        else if(result == "waiting")
        {
            Debug.Log("waiting");
            userInfoDetailScript.addFriendBtn.interactable = false;
            userInfoDetailScript.addFriendText.text = "수락대기";
        }
        // 친구 요청이 들어와서 수락 누를 수 있는 상태
        else if(result == "accept")
        {
            Debug.Log("accept");
            userInfoDetailScript.addFriendBtn.interactable = true;
            userInfoDetailScript.addFriendText.text = "수락";
        }
        // 아무 상태도 아님
        else if(result == "notFriend")
        {
            Debug.Log("notFriendㅋㅋ");
            userInfoDetailScript.addFriendBtn.interactable = true;
            userInfoDetailScript.addFriendText.text = "친구추가";
            userInfoDetailScript.addFriendBtn.onClick.AddListener(() => StartCoroutine(clickAddFriendBtn()));
        }
    }

    // 친구 추가 호출
    private IEnumerator clickAddFriendBtn()
    {
        yield return StartCoroutine(lobbyManagerScript.addFriend(Id));

        // 친구인지 확인 후 친구추가 버튼 바꾸기
        StartCoroutine(setFriendStatus());
    }

    // TODO: 친구 수락 호출
    private IEnumerator clickAcceptFriend()
    {
        yield break;
    }
}
