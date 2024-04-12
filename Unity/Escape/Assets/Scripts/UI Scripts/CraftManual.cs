using Polyperfect.Crafting.Integration;
using System.Collections;
using System.Collections.Generic;
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
    // ���� �κ��丮�� �߰� �� ������
    public Item go_Prefab;
}

public class CraftManual : MonoBehaviour
{
    // ���� ����
    public static bool isActivated = false;

    [SerializeField]
    private GameObject go_BaseUI;

    private int tabNumber = 0;
    private int page = 1;
    private int selectedSlotNumber;
    private Craft[] craft_SelectedTab;

    // Ż�� ��� ��
    [SerializeField]
    private Craft[] craft_ingredient;
    // ����� ��
    [SerializeField]
    private Craft[] craft_weapon;
    // ����� ���� ��
    [SerializeField]
    private Craft[] craft_escape;

    // �̸����� �������� ���� ����
    private GameObject go_Preview;
    // ���� ������ �������� ���� ����
    private Item go_Prefab;
    // ��� �޼���
    [SerializeField]
    private TextMeshProUGUI warningMsg;
    // ���̵� ��, ���̵� �ƿ��� �ð�
    private float time = 0f;
    private float F_time = 0.5f;

    // �ʿ��� UI ���� ���
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

    // �ʿ��� ������Ʈ
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

        // 4�� ���
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
        }

        return true;
    }

    private void UseIngredient()
    {
        Score.score += 5;
        for (int i = 0; i < craft_SelectedTab[selectedSlotNumber].craftNeedItem.Length; i++)
        {
            theInventory.SetItemCount(craft_SelectedTab[selectedSlotNumber].craftNeedItem[i], craft_SelectedTab[selectedSlotNumber].craftNeedItemCount[i]);
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        //{
        //    Window();
        //}
    }

    public void Window()
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
