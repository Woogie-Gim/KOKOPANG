using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private GameObject survivalTimer;

    [SerializeField]
    private TextMeshProUGUI[] text_Msg;

    // 페이드 인, 페이드 아웃용 시간
    private float time = 0f;
    private float F_time = 5f;

    // Start is called before the first frame update
    void Start()
    {
        survivalTimer.SetActive(true);
        Fade1();
    }


    public void Fade1()
    {
        StartCoroutine(FadeInFlow1());
    }
    IEnumerator FadeInFlow1()
    {
        text_Msg[0].gameObject.SetActive(true);
        Color alpha = text_Msg[0].color;
        float startTime = Time.time; // 현재 시간 기록
        float elapsedTime = 0f;

        while (alpha.a < 1f)
        {
            elapsedTime = Time.time - startTime; // 경과 시간 계산
            float t = elapsedTime / F_time; // 경과 시간을 전체 시간으로 나누어서 보간값 계산
            alpha.a = Mathf.Lerp(0, 1, t); // 보간값 적용
            text_Msg[0].color = alpha;
            yield return null;
        }

        // FadeInFlow() 코루틴이 완료되면 FadeOutFlow() 시작
        StartCoroutine(FadeOutFlow1());
    }

    IEnumerator FadeOutFlow1()
    {
        Color alpha = text_Msg[0].color;
        float startTime = Time.time; // 현재 시간 기록
        float elapsedTime = 0f;

        while (alpha.a > 0f)
        {
            elapsedTime = Time.time - startTime; // 경과 시간 계산
            float t = elapsedTime / F_time; // 경과 시간을 전체 시간으로 나누어서 보간값 계산
            alpha.a = Mathf.Lerp(1, 0, t); // 보간값 적용
            text_Msg[0].color = alpha;
            yield return null;
        }

        text_Msg[0].gameObject.SetActive(false);
    }
}
