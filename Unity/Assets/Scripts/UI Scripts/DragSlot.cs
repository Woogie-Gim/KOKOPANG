using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public Slot dragSlot;

    // 아이템 이미지
    [SerializeField]
    private Image imageItem;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite;
        SetColor(1);
    }

    public void ClearImage(Image _itemImage)
    {
        imageItem.sprite = _itemImage.sprite;
        SetColor(0);
    }

    public void SetColor(float _alphha)
    {
        Color color = imageItem.color;
        color.a = _alphha;
        imageItem.color = color;
    }
}
