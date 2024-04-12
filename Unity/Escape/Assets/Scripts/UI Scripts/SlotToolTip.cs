using TMPro;
using UnityEngine;

public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private TMP_Text ItemName;
    [SerializeField]
    private TMP_Text ItemDesc;
    [SerializeField]
    private TMP_Text ItemHowToUse;

    public void ShowToolTip(Item _item, Vector3 _pos)
    {
        go_Base.SetActive(true);
        _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.5f - 50f, -go_Base.GetComponent<RectTransform>().rect.height * 0.5f + 100f, 0f);
        go_Base.transform.position = _pos;

        ItemName.text = _item.itemName;
        ItemDesc.text = _item.itemDescription;

        if (_item.itemType == Item.ItemType.Equipment)
        {
            ItemHowToUse.text = "우클릭 - 장착";
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            ItemHowToUse.text = "우클릭 - 먹기";
        }
        else
        {
            ItemHowToUse.text = "";
        }
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}
