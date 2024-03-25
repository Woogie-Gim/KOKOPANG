using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeController : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = true;
    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if (hitInfo.transform.tag == "Rock")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining();
                }
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
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
