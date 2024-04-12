using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInfoDetail : MonoBehaviour
{
    public TMP_Text DetailUserNameText;
    //public TMP_Text DetailEmailText;
    public Button AddFriendBtn;
    public TMP_Text AddFriendText;
    public Button DetailCloseBtn;

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
        // �̺�Ʈ ���̱�
        DetailCloseBtn.onClick.AddListener(clickCloseBtn);
    }

    // ���� �� ������ �ݱ� ��ư Ŭ�� ��
    public void clickCloseBtn()
    {
        gameObject.SetActive(false);
    }
}
