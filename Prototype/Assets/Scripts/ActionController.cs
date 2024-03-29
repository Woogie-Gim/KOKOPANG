using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionController : MonoBehaviour
{
    // 습득 가능한 초ㅓㅣ대 거리
    [SerializeField]
    private float range;

    // 습득 가능할 시 trye
    private bool pickupActivated = false;

    // 충돌체 정보 저장
    private RaycastHit hitInfo;

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정
    [SerializeField]
    private LayerMask layerMask;

   // 필요한 컴포넌트
    [SerializeField]
    private TMP_Text actionText;
    [SerializeField]
    private Inventory theInventory;

    // Update is called once per frame
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            InfoDisappear();
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        ItemPickup itemPickup = hitInfo.transform.GetComponent<ItemPickup>();
        if (itemPickup != null && itemPickup.item != null)
        {
            actionText.text = itemPickup.item.itemName + " 획득 " + "<color=yellow>" + "(E)" + "</color>";
        } 
    }

    private void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            ItemPickup itemPickup = hitInfo.transform.GetComponent<ItemPickup>();
            if (itemPickup != null && itemPickup.item != null)
            {
                theInventory.AcquireItem(itemPickup.item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }
}
