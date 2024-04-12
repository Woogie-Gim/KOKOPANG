using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserListElement : MonoBehaviour
{
    public GameObject UserInfoDetail;

    public LobbyManager lobbyManagerScript;

    public TMP_Text UserNameText;
    public Button DetailBtn;

    [SerializeField] private int id;
    [SerializeField] private string email;
    [SerializeField] private new string name;


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
        // �̺�Ʈ ���̱�
        DetailBtn.onClick.AddListener(clickOpenUserDetailBtn);

        lobbyManagerScript = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
    }

    // ���� �� ������ ���� ��ư Ŭ�� ��
    public void clickOpenUserDetailBtn()
    {
        UserInfoDetail script = UserInfoDetail.GetComponent<UserInfoDetail>();
        UserInfoDetail.SetActive(true);

        script.Email = Email;
        script.Name = Name;
        script.Id = Id;

        script.DetailUserNameText.text = Name + " #" + Id;
        //script.DetailEmailText.text = Email;

        // ģ������ Ȯ�� �� ģ���߰� ��ư �ٲٱ�
        StartCoroutine(setFriendStatus());
    }

    // ģ�� ���º� ��ư �ٲٱ�
    public IEnumerator setFriendStatus()
    {
        string result = "";
        yield return StartCoroutine(lobbyManagerScript.FriendCheckRequest(Id, value => result = value));

        UserInfoDetail userInfoDetailScript = UserInfoDetail.GetComponent<UserInfoDetail>();

        // ���� ģ��
        if (result == "friend")
        {
            //Debug.Log("friend");
            userInfoDetailScript.AddFriendBtn.interactable = false;
            userInfoDetailScript.AddFriendText.text = "ģ��";
        }
        // ���� ��û ������ ���� �ޱ� �����
        else if(result == "waiting")
        {
            //Debug.Log("waiting");
            userInfoDetailScript.AddFriendBtn.interactable = false;
            userInfoDetailScript.AddFriendText.text = "�������";
        }
        // ģ�� ��û�� ���ͼ� ���� ���� �� �ִ� ����
        else if(result == "accept")
        {
            //Debug.Log("accept");
            userInfoDetailScript.AddFriendBtn.interactable = true;
            userInfoDetailScript.AddFriendText.text = "����";
            userInfoDetailScript.AddFriendBtn.onClick.RemoveAllListeners();
            userInfoDetailScript.AddFriendBtn.onClick.AddListener(() => StartCoroutine(clickAcceptFriendBtn()));
        }
        // �ƹ� ���µ� �ƴ�
        else if(result == "notFriend")
        {
            //Debug.Log("notFriend����");
            userInfoDetailScript.AddFriendBtn.interactable = true;
            userInfoDetailScript.AddFriendText.text = "ģ���߰�";
            userInfoDetailScript.AddFriendBtn.onClick.RemoveAllListeners();
            userInfoDetailScript.AddFriendBtn.onClick.AddListener(() => StartCoroutine(clickAddFriendBtn()));
        }
    }

    // ģ�� �߰� ȣ��
    private IEnumerator clickAddFriendBtn()
    {
        yield return StartCoroutine(lobbyManagerScript.addFriend(Id));

        // ģ������ Ȯ�� �� ģ���߰� ��ư �ٲٱ�
        StartCoroutine(setFriendStatus());
    }

    // ģ�� ���� ȣ��
    private IEnumerator clickAcceptFriendBtn()
    {
        yield return StartCoroutine(lobbyManagerScript.acceptFriend(Id));

        // ģ������ Ȯ�� �� ģ���߰� ��ư �ٲٱ�
        StartCoroutine(setFriendStatus());
    }
}
