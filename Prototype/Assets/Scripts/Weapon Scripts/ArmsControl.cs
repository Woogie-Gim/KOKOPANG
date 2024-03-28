using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsControl : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = false;

    [SerializeField]
    private QuickSlotController theQuickSlotController;
    void Update()
    {
        if (isActivate && !Inventory.inventoryActivated)
        {
            if (QuickSlotController.go_HandItem == null)
            {
                TryAttack();
            }
            else
            {
                TryEating();
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
            currentArms.anim.SetTrigger("Eat");
            theQuickSlotController.EatItem();
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
