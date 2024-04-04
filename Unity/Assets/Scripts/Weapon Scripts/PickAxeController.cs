using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeController : CloseWeaponController
{
    // 활성화 여부
    public static bool isActivate = true;

    public bool isMine = false;

    void Update()
    {
        if(isMine)
        {
            if (isActivate)
            {
                TryAttack();
            }
        }
    }

    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if (hitInfo.transform != null && hitInfo.transform.tag == "Rock")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining();
                }
                else if (hitInfo.transform != null && hitInfo.transform.tag == "Ore")
                {
                    hitInfo.transform.GetComponent<Ore>().Mining();
                }
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
