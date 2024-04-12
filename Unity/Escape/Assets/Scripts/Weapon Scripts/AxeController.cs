using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : CloseWeaponController
{
    // Ȱ��ȭ ����
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
                if (hitInfo.transform.tag == "Tree")
                {
                    hitInfo.transform.GetComponent<Tree>().Felling();
                }
                if (hitInfo.transform.tag == "Barrel")
                {
                    hitInfo.transform.GetComponent<Barrel>().Felling();
                }
                else if (hitInfo.transform.tag == "Pig")
                {
                    hitInfo.transform.GetComponent<Pig>().Damage(transform.position);
                }
                else if (hitInfo.transform.tag == "Cow")
                {
                    hitInfo.transform.GetComponent<Cow>().Damage(transform.position);
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
