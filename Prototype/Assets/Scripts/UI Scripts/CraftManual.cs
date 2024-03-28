using Polyperfect.Crafting.Integration;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CraftingAnims;

[System.Serializable]
public class Craft
{
    public string craftName;
    public Sprite craftImage;
    public string craftDescription;
    public string[] craftNeedItem;
    public int[] craftNeedItemCount;
    // 실제 인벤토리에 추가 될 프리팹
    public Item go_Prefab;
}

public class CraftManual : MonoBehaviour
{
    // 상태 변수
    public static bool isActivated = false;

    [SerializeField]
    private GameObject go_BaseUI;

    private int tabNumber = 0;
    private int page = 1;
    private int selectedSlotNumber;
    private Craft[] craft_SelectedTab;

    // 탈출 재료 탭
    [SerializeField]
    private Craft[] craft_ingredient;
    // 무기용 탭
    [SerializeField]
    private Craft[] craft_weapon;
    // 비행기 수리 탭
    [SerializeField]
    private Craft[] craft_escape;

    // 미리보기 프리팹을 담을 변수
    private GameObject go_Preview;
    // 실제 생성될 프리팹을 담을 변수
    private Item go_Prefab;
    // 경고 메세지
    [SerializeField]
    private TextMeshProUGUI warningMsg;
    // 페이드 인, 페이드 아웃용 시간
    private float time = 0f;
    private float F_time = 0.5f;

    // 필요한 UI 슬롯 요소
    [SerializeField]
    private GameObject[] go_Slots;
    [SerializeField]
    private Image[] image_Slot;
    [SerializeField]
    private TextMeshProUGUI[] text_SlotName;
    [SerializeField]
    private TextMeshProUGUI[] text_SlotDescription;
    [SerializeField]
    private TextMeshProUGUI[] text_SlotNeedItem;

    // 필요한 컴포넌트
    private Inventory theInventory;


    void Start()
    {
        theInventory = FindAnyObjectByType<Inventory>();
        tabNumber = 0;
        page = 1;
        TabSlotSetting(craft_ingredient);
    }

    public void TabSetting(int _tabNumber)
    {
        tabNumber = _tabNumber;
        page = 1;

        switch (tabNumber)
        {
            case 0:
                TabSlotSetting(craft_ingredient);
                break;
            case 1:
                TabSlotSetting(craft_weapon);
                break;
            case 2:
                TabSlotSetting(craft_escape);
                break;
        }
    }

    public void RightPageSetting()
    {
        if (page < (craft_SelectedTab.Length / go_Slots.Length) + 1)
        {
            page++;
        }
        else
        {
            page = 1;
        }

        TabSlotSetting(craft_SelectedTab);
    }
    public void LeftPageSetting()
    {
        if (page != 1)
        {
            page--;
        }
        else
        {
            page = (craft_SelectedTab.Length / go_Slots.Length) + 1;
        }

        TabSlotSetting(craft_SelectedTab);
    }

    private void ClearSlot()
    {
        for (int i = 0; i < go_Slots.Length; i++)
        {
            image_Slot[i].sprite = null;
            text_SlotName[i].text = "";
            text_SlotDescription[i].text = "";
            text_SlotNeedItem[i].text = "";
            go_Slots[i].SetActive(false);
        }
    }

    private void TabSlotSetting(Craft[] _craft_tab)
    {
        ClearSlot();
        craft_SelectedTab = _craft_tab;

        // 4의 배수
        int startSlotNumber = (page - 1) * go_Slots.Length;

        for (int i = startSlotNumber; i < craft_SelectedTab.Length; i++)
        {
            if (i == page * go_Slots.Length) break;

            go_Slots[i - startSlotNumber].SetActive(true);

            image_Slot[i - startSlotNumber].sprite = craft_SelectedTab[i].craftImage;
            text_SlotName[i - startSlotNumber].text = craft_SelectedTab[i].craftName;
            text_SlotDescription[i - startSlotNumber].text = craft_SelectedTab[i].craftDescription;

            for (int j = 0; j < craft_SelectedTab[i].craftNeedItem.Length; j++)
            {
                text_SlotNeedItem[i - startSlotNumber].text += craft_SelectedTab[i].craftNeedItem[j] + " ";
                text_SlotNeedItem[i - startSlotNumber].text += craft_SelectedTab[i].craftNeedItemCount[j] + "\n";
            }
        }
    }

    public void SlotClick(int _slotNumber)
    {
        selectedSlotNumber = _slotNumber + (page - 1) * go_Slots.Length;

        if (!CheckIngredient())
        {
            Fade();
            return;
        }

        UseIngredient();
        go_Prefab = craft_SelectedTab[selectedSlotNumber].go_Prefab;
        theInventory.AcquireItem(go_Prefab);
        isActivated = false;
        go_BaseUI.SetActive(false);
    }

    private bool CheckIngredient()
    {
        for (int i = 0; i < craft_SelectedTab[selectedSlotNumber].craftNeedItem.Length; i++)
        {
            if (theInventory.GetItemCount(craft_SelectedTab[selectedSlotNumber].craftNeedItem[i]) < craft_SelectedTab[selectedSlotNumber].craftNeedItemCount[i])
            {
                return false;
            }

            return true;
        }

        return true;
    }

    private void UseIngredient()
    {
        for (int i = 0; i < craft_SelectedTab[selectedSlotNumber].craftNeedItem.Length; i++)
        {
            theInventory.SetItemCount(craft_SelectedTab[selectedSlotNumber].craftNeedItem[i], craft_SelectedTab[selectedSlotNumber].craftNeedItemCount[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Window();
        }
    }

    private void Window()
    {
        if (!isActivated)
        {
            OpenWindow();
        }
        else
        {
            CloseWindow();
        }
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }
    private void CloseWindow()
    {
        isActivated = false;
        go_BaseUI.SetActive(false);
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
