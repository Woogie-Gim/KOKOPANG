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
    // ���� ����
    public static bool isActivated = false;

    [SerializeField]
    private GameObject go_BaseUI;

    private int selectedSlotNumber;

    // Ż�� ��� ��
    [SerializeField]
    private Fix[] fix_ingredient;

    // ��� �޼���
    [SerializeField]
    private TextMeshProUGUI warningMsg;
    // ���̵� ��, ���̵� �ƿ��� �ð�
    private float time = 0f;
    private float F_time = 1f;

    // �ʿ��� UI ���� ���
    [SerializeField]
    private GameObject[] go_Slots;

    // �ʿ��� ������Ʈ
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
        float startTime = Time.time; // ���� �ð� ���
        float elapsedTime = 0f;

        while (alpha.a < 1f)
        {
            elapsedTime = Time.time - startTime; // ��� �ð� ���
            float t = elapsedTime / F_time; // ��� �ð��� ��ü �ð����� ����� ������ ���
            alpha.a = Mathf.Lerp(0, 1, t); // ������ ����
            warningMsg.color = alpha;
            yield return null;
        }

        // FadeInFlow() �ڷ�ƾ�� �Ϸ�Ǹ� FadeOutFlow() ����
        StartCoroutine(FadeOutFlow());
    }

    IEnumerator FadeOutFlow()
    {
        Color alpha = warningMsg.color;
        float startTime = Time.time; // ���� �ð� ���
        float elapsedTime = 0f;

        while (alpha.a > 0f)
        {
            elapsedTime = Time.time - startTime; // ��� �ð� ���
            float t = elapsedTime / F_time; // ��� �ð��� ��ü �ð����� ����� ������ ���
            alpha.a = Mathf.Lerp(1, 0, t); // ������ ����
            warningMsg.color = alpha;
            yield return null;
        }

        warningMsg.gameObject.SetActive(false);
    }
}