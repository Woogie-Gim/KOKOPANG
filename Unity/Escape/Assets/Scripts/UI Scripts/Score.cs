using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;

    [SerializeField]
    private TextMeshProUGUI score_txt;
    public static int score;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score_txt.text = "Score : " + "<color=yellow>" + score.ToString() + "</color>";
        DataManager.Instance.score[DataManager.Instance.myIdx] = score;
    }

    public int GetScore()
    {
        return score;
    }
}
