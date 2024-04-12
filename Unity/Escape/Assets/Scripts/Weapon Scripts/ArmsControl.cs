using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsControl : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = false;
    [SerializeField]
    private AudioSource armsaudioSource;
    [SerializeField] 
    private AudioClip eatSound;

    [SerializeField]
    private QuickSlotController theQuickSlotController;

    public bool isMine = false; // 해당 캐릭터가 플레이어 자신인지 확인

    void Update()
    {
        if(isMine)
        {
            if (isActivate && !Inventory.inventoryActivated)
            {
                if (QuickSlotController.go_HandItem == null)
                {
                    TryAttack();
                }
                else if(QuickSlotController.quickSlots[QuickSlotController.selectedSlot].item.itemType == Item.ItemType.Used)
                {
                    TryEating();
                }
                else
                {
                    TryAttack();
                }
            }
        }
    }
     void Start()
    {
        WeaponManager.currentWeapon = currentArms.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentArms.anim;
        isActivate = true;
    }

    private void TryEating()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //if(QuickSlotController.quickSlots[QuickSlotController.selectedSlot].item.itemType == Item.ItemType.Used)
            //{
            //}

            Score.score += 2;
            armsaudioSource.Play();
            currentArms.anim.SetTrigger("Eat");
            theQuickSlotController.EatItem();
            armsaudioSource.clip = eatSound;
        }
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
            }
            yield return null;
        }
    }
    public override void ArmsChange(Arms _arms)
    {
        base.ArmsChange(_arms);
        isActivate = true;
    }
}
