using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Fix
{
    public string[] craftNeedItem;
    public int[] craftNeedItemCount;
}

public class FixManual : MonoBehaviour
{
    // 상태 변수
    public static bool isActivated = false;

    [SerializeField]
    private GameObject go_BaseUI;

    private int selectedSlotNumber;

    // 탈출 재료 탭
    [SerializeField]
    private Fix[] fix_ingredient;

    // 경고 메세지
    [SerializeField]
    private TextMeshProUGUI warningMsg;
    // 페이드 인, 페이드 아웃용 시간
    private float time = 0f;
    private float F_time = 1f;

    // 필요한 UI 슬롯 요소
    [SerializeField]
    private GameObject[] go_Slots;

    // 필요한 컴포넌트
    private Inventory theInventory;
    private TimerController theTimerController;
    private FinalSpawnController theFinalSpawnController;

    void Start()
    {
        theInventory = FindAnyObjectByType<Inventory>();
        theTimerController = FindAnyObjectByType<TimerController>();
        theFinalSpawnController = FindAnyObjectByType<FinalSpawnController>();
    }

    public void SlotClick()
    {
        if (!CheckIngredient())
        {
            Fade();
            return;
        }

        UseIngredient();
        isActivated = false;
        FixTab.fixtabActivated = false;
        Destroy(gameObject);
        go_BaseUI.SetActive(false);

        TCPConnectManager.Instance.gameClear();
    }

    private bool CheckIngredient()
    {
        for (int i = 0; i < fix_ingredient[0].craftNeedItem.Length; i++)
        {
            if (theInventory.GetItemCount(fix_ingredient[0].craftNeedItem[i]) < fix_ingredient[0].craftNeedItemCount[i])
            {
                return false;
            }

            //return true;
        }

        return true;
    }

    private void UseIngredient()
    {
        Score.score += 10;

        for (int i = 0; i < fix_ingredient[0].craftNeedItem.Length; i++)
        {
            theInventory.SetItemCount(fix_ingredient[0].craftNeedItem[i], fix_ingredient[0].craftNeedItemCount[i]);
        }
    }

    public void Fade()
    {
        StartCoroutine(FadeInFlow());
    }


    IEnumerator FadeInFlow()
    {
        warningMsg.gameObject.SetActive(true);
        Color alpha = warningMsg.color;
        float startTime = Time.time; // 현재 시간 기록
        float elapsedTime = 0f;

        while (alpha.a < 1f)
        {
            elapsedTime = Time.time - startTime; // 경과 시간 계산
            float t = elapsedTime / F_time; // 경과 시간을 전체 시간으로 나누어서 보간값 계산
            alpha.a = Mathf.Lerp(0, 1, t); // 보간값 적용
            warningMsg.color = alpha;
            yield return null;
        }

        // FadeInFlow() 코루틴이 완료되면 FadeOutFlow() 시작
        StartCoroutine(FadeOutFlow());
    }

    IEnumerator FadeOutFlow()
    {
        Color alpha = warningMsg.color;
        float startTime = Time.time; // 현재 시간 기록
        float elapsedTime = 0f;

        while (alpha.a > 0f)
        {
            elapsedTime = Time.time - startTime; // 경과 시간 계산
            float t = elapsedTime / F_time; // 경과 시간을 전체 시간으로 나누어서 보간값 계산
            alpha.a = Mathf.Lerp(1, 0, t); // 보간값 적용
            warningMsg.color = alpha;
            yield return null;
        }

        warningMsg.gameObject.SetActive(false);
    }
}