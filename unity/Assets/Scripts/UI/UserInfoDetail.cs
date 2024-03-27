using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInfoDetail : MonoBehaviour
{
    public TMP_Text detailUserNameText;
    public TMP_Text detailEmailText;
    public Button addFriendBtn;
    public TMP_Text addFriendText;
    public Button detailCloseBtn;

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
        detailCloseBtn.onClick.AddListener(clickCloseBtn);
    }

    // 유저 상세 페이지 닫기 버튼 클릭 시
    public void clickCloseBtn()
    {
        gameObject.SetActive(false);
    }
}
