using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector3 originPos;

    // 획득한 아이템
    public Item item;
    // 획득한 아이템의 개수
    public int itemCount;
    // 아이템의 이미지
    public Image itemImage;

    // 퀵슬롯 여부 판단
    [SerializeField]
    private bool isQuickSlot;
    // 퀵슬롯 번호
    [SerializeField]
    private int quickSlotNumber;

    // 필요한 컴포넌트
    [SerializeField]
    private TMP_Text text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    // 인벤토리 영역
    [SerializeField]
    private RectTransform baseRect;
    //퀵 슬롯의 영역
    [SerializeField]
    private RectTransform quickSlotBaseRect;
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

    // 이미지 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
    
    // 아이템 획득
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

    // 아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            ClearSlot();
        }
    }

    // 슬롯 초기화
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
                    // 장착
                }
                else
                {
                    // 소모
                    Debug.Log(item.itemName + " 을 사용했습니다.");
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
        // 인벤토리 최대, 최소 영역을 벗어났을 때
        if (!((DragSlot.instance.transform.localPosition.x > baseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < baseRect.rect.xMax 
            && DragSlot.instance.transform.localPosition.y > baseRect.rect.yMin && DragSlot.instance.transform.localPosition.y < baseRect.rect.yMax)
            ||
            (DragSlot.instance.transform.localPosition.x > quickSlotBaseRect.rect.xMin && DragSlot.instance.transform.localPosition.x < quickSlotBaseRect.rect.xMax &&
            DragSlot.instance.transform.localPosition.y > quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMax - baseRect.localPosition.y && DragSlot.instance.transform.localPosition.y < quickSlotBaseRect.transform.localPosition.y - quickSlotBaseRect.rect.yMin - baseRect.localPosition.y)))
        {
            if (DragSlot.instance.dragSlot != null)
            {
                theInputNumber.Call();
            }
        }
        else
        {
            DragSlot.instance.SetColor(0);
            DragSlot.instance.dragSlot = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragSlot != null)
        {
            ChangeSlot();
            
            // 인벤토리에서 퀵슬롯으로 (혹은 퀵슬롯에서 퀵슬롯으로)
            if (isQuickSlot)
            {
                // 활성화된 퀵슬롯 ? 교체 작업
                theQuickSlotController.IsActivatedQuickSLot(quickSlotNumber);
            }
            // 인벤토리 -> 인벤토리, 퀵슬롯 -> 퀵슬롯
            else
            {
                // 퀵슬롯 -> 인벤토리
                if (DragSlot.instance.dragSlot.isQuickSlot)
                {
                    // 활성화된 퀵슬롯 ? 교체 작업
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

    // 마우스가 슬롯에 들어갈 때 발동 
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            theSlotToolTip.ShowToolTip(item, transform.position);
        }
    }

    // 슬롯에서 빠져 나갈 때 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        theSlotToolTip.HideToolTip();
    }
}
