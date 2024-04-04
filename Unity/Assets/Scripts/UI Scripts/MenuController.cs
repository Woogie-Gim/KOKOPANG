using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private GameObject tutorial;

    public static bool isMenu = false;
    public static bool isTutorial = false;

    void Start()
    {
        Menu.SetActive(false);
        tutorial.SetActive(false);
    }

    void Update()
    {
        //if (!isMenu && Input.GetKeyUp(KeyCode.Escape))
        //{
        //    OpenMenu();
        //    isMenu = true;
        //}
        //else if (isMenu && Input.GetKeyUp(KeyCode.Escape))
        //{
        //    CloseMenu();
        //    isMenu = false;

        //    if (isTutorial)
        //    {
        //        CloseTutorial();
        //    }
        //}
    }

    public void OpenMenu()
    {
        if (!isMenu)
        {
            Menu.SetActive(true);
            isMenu = true;
        }

    }
    public void CloseMenu()
    {
        if (isMenu)
        {
            Menu.SetActive(false);
            isMenu = false;
        }

    }

    public void OpenTutorial()
    {
        if (!isTutorial)
        {
            tutorial.SetActive(true);
            isTutorial = true; 
        }
    }

    public void CloseTutorial()
    {
        if (isTutorial)
        {
            tutorial.SetActive(false);
            isTutorial = false;
        }
    }

    public void GotoLobby()
    {
        //TCPConnectManager.Instance.exitGameScene(); // 서버에 채널 나가기 요청
        TCPConnectManager.Instance.OnApplicationQuit();
        SceneManager.LoadScene("Login");
    }
}
