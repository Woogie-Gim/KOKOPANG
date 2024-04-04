using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorial;

    void Start()
    {
        tutorial.SetActive(false);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OpenTutorial()
    {
        tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
    }
}
