using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsControl : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = false;
    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }
    }
     void Start()
    {
        WeaponManager.currentWeapon = currentArms.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentArms.anim;
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
                if (hitInfo.transform.tag == "NPC")
                {
                    hitInfo.transform.GetComponent<Pig>().Damage(currentArms.damage, transform.position);
                }
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
