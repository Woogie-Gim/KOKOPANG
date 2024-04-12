using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuickSlotController : MonoBehaviour
{
    // �� ���Ե�
    [SerializeField]
    public static Slot[] quickSlots;
    // �������� �θ�ü
    [SerializeField]
    private Transform tf_parent;
    // �������� ��ġ�� �� ��
    [SerializeField]
    private Transform tf_ItemPos;
    // �տ� �� ������
    public static GameObject go_HandItem;

    // ���õ� ������ (0 ~ 7) = 8��
    public static int selectedSlot;
    // ���õ� �������� �̹���
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
        // ���õ� ����
        selectedSlot = _num;

        // ���õ� �������� �̹��� �̵�
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
        StartCoroutine(theWeaponManager.ChangeWeaponCoroutine("ARMS", "�Ǽ�"));

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
        go_HandItem.GetComponent<BoxCollider>().enabled = false;
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
