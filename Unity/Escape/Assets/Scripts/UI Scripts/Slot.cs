using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 originPos;

    // ȹ���� ������
    public Item item;
    // ȹ���� �������� ����
    public int itemCount;
    // �������� �̹���
    public Image itemImage;

    // ������ ���� �Ǵ�
    [SerializeField]
    private bool isQuickSlot;
    // ������ ��ȣ
    [SerializeField]
    private int quickSlotNumber;

    // �ʿ��� ������Ʈ
    [SerializeField]
    private TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    // �κ��丮 ����
    [SerializeField]
    private RectTransform baseRect;
    // �� ������ ����
    [SerializeField]
    private RectTransform quickSlotBaseRect;
    // ���� ������ ����
    [SerializeField]
    private RectTransform fixRect;

    private InputNumber theInputNumber;
    private SlotToolTip theSlotToolTip;
    private QuickSlotController theQuickSlotController;

    void Start()
    {
        originPos = transform.position;
        theInputNumber = FindObjectOfType<InputNumber>();
        theSlotToolTip = FindObjectOfType<SlotToolTip>();
        theQuickSlotController = FindObjectOfType<QuickSlotController>();
    }

    // �̹��� ���� ����
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    
    // ������ ȹ��
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }
        SetColor(1);
    }

    public int GetQuickSlotNumber()
    {
        return quickSlotNumber;
    }

    // ������ ���� ����
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    // ���� �ʱ�ȭ
    private void ClearSlot() 
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    // ����
                }
                else
                {
                    // �Ҹ�
                    Debug.Log(item.itemName + " �� ����߽��ϴ�.");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �κ��丮 �ִ�, �ּ� ������ ����� ��
        if (DragSlot.instance != null && DragSlot.instance.dragSlot != null)
        {
            if (!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax 
                && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
                ||
                (DragSlot.instance.transform.localPosition.x > quickSlotBaseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax &&
                DragSlot.instance.transform.localPosition.y > quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMax - baseRect.localPosition.y && DragSlot.instance.transform.localPosition.y < quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMin - baseRect.localPosition.y))
                )
            {
                if (DragSlot.instance.dragSlot != null)
                {
                    //theInputNumber.Call();
                    DragSlot.instance.ClearImage(itemImage);
                    return;
                }
            }
            else
            {
                DragSlot.instance.SetColor(0);
                DragSlot.instance.dragSlot = null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
            
            // �κ��丮���� ���������� (Ȥ�� �����Կ��� ����������)
            if (isQuickSlot)
            {
                // Ȱ��ȭ�� ������ ? ��ü �۾�
                theQuickSlotController.IsActivatedQuickSLot(quickSlotNumber);
            }
            // �κ��丮 -> �κ��丮, ������ -> ������
            else
            {
                // ������ -> �κ��丮
                if (DragSlot.instance.dragSlot.isQuickSlot)
                {
                    // Ȱ��ȭ�� ������ ? ��ü �۾�
                    theQuickSlotController.IsActivatedQuickSLot(DragSlot.instance.dragSlot.quickSlotNumber);
                }
            }
        }

    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }

    // ���콺�� ���Կ� �� �� �ߵ� 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            theSlotToolTip.ShowToolTip(item, transform.position);
        }
    }

    // ���Կ��� ���� ���� �� �ߵ�
    public void OnPointerExit(PointerEventData eventData)
    {
        theSlotToolTip.HideToolTip();
    }
}
