using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    // 퀵 슬롯들
    [SerializeField]
    private Slot[] quickSlots;
    // 퀵슬롯의 부모객체
    [SerializeField]
    private Transform tf_parent;
    // 아이템이 위치할 손 끝
    [SerializeField]
    private Transform tf_ItemPos;
    // 손에 든 아이템
    public static GameObject go_HandItem;

    // 선택된 퀵슬롯 (0 ~ 7) = 8개
    private int selectedSlot;
    // 선택된 퀵슬롯의 이미지
    [SerializeField]
    private GameObject go_SelectedImage;
    [SerializeField]
    private WeaponManager theWeaponManager;

    // Start is called before the first frame update
    void Start()
    {
        quickSlots = tf_parent.GetComponentsInChildren<Slot>();
        selectedSlot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TryInputNumber();
    }

    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeSlot(7);
        }
    }
    public void IsActivatedQuickSLot(int _num)
    {
        if (selectedSlot == _num)
        {
            Execute();
            return;
        }
        if (DragSlot.instance != null)
        {
            if (DragSlot.instance.dragSlot != null)
            {
                if (DragSlot.instance.dragSlot.GetQuickSlotNumber() == selectedSlot)
                {
                    Execute();
                    return;
                }
            }
        }
    }
    private void ChangeSlot(int _num) 
    {
        SelectedSlot(_num);

        Execute();
    }

    private void SelectedSlot(int _num)
    {
        // 선택된 슬롯
        selectedSlot = _num;

        // 선택된 슬롯으로 이미지 이동
        go_SelectedImage.transform.position = quickSlots[selectedSlot].transform.position;
    }

    private void Execute()
    {
        if (quickSlots[selectedSlot].item != null)
        {
            if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Equipment)
            {
                StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(quickSlots[selectedSlot].item.weaponType, quickSlots[selectedSlot].item.itemName));
            }
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Used)
            {
                ChangeHand(quickSlots[selectedSlot].item);
            }
            else if (quickSlots[selectedSlot].item.itemType == Item.ItemType.Hammer)
            {
                ChangeHand(quickSlots[selectedSlot].item);
            }
            else
            {
                ChangeHand();
            }
        }
        else
        {
            ChangeHand();
        }
    }

    private void ChangeHand(Item _item = null)
    {
        StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("ARMS", "맨손"));

        if (_item != null)
        {
            StartCoroutine(HandItemCoroutine());
        }
    }

    IEnumerator HandItemCoroutine()
    {
        ArmsControl.isActivate = false;
        yield return new WaitUntil(() => ArmsControl.isActivate);

        go_HandItem = Instantiate(quickSlots[selectedSlot].item.itemPrefab, tf_ItemPos.position, tf_ItemPos.rotation);
        go_HandItem.GetComponent<Rigidbody>().isKinematic = true;
        go_HandItem.GetComponent<MeshCollider>().enabled = false;
        go_HandItem.tag = "Untagged";
        go_HandItem.layer = 7;
        go_HandItem.transform.SetParent(tf_ItemPos);
    }

    public void EatItem()
    {
        if (quickSlots[selectedSlot].item.itemType != Item.ItemType.Hammer)
        {
            quickSlots[selectedSlot].SetSlotCount(-1);

            if (quickSlots[selectedSlot].itemCount <= 0)
            {
                Destroy(go_HandItem);
            }
        }
    }
}
