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

    // ���̵� ��, ���̵� �ƿ��� �ð�
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
        float startTime = Time.time; // ���� �ð� ���
        float elapsedTime = 0f;

        while (alpha.a < 1f)
        {
            elapsedTime = Time.time - startTime; // ��� �ð� ���
            float t = elapsedTime / F_time; // ��� �ð��� ��ü �ð����� ����� ������ ���
            alpha.a = Mathf.Lerp(0, 1, t); // ������ ����
            text_Msg[0].color = alpha;
            yield return null;
        }

        // FadeInFlow() �ڷ�ƾ�� �Ϸ�Ǹ� FadeOutFlow() ����
        StartCoroutine(FadeOutFlow1());
    }

    IEnumerator FadeOutFlow1()
    {
        Color alpha = text_Msg[0].color;
        float startTime = Time.time; // ���� �ð� ���
        float elapsedTime = 0f;

        while (alpha.a > 0f)
        {
            elapsedTime = Time.time - startTime; // ��� �ð� ���
            float t = elapsedTime / F_time; // ��� �ð��� ��ü �ð����� ����� ������ ���
            alpha.a = Mathf.Lerp(1, 0, t); // ������ ����
            text_Msg[0].color = alpha;
            yield return null;
        }

        text_Msg[0].gameObject.SetActive(false);
    }
}
