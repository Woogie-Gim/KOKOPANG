using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    // 현재 장착된 Arms형 타입 무기
    [SerializeField]
    protected Arms currentArms;

    // 공격 중 상태
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;
    protected void TryAttack()
    {
        if (!Inventory.inventoryActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    // 코루틴 실행
                    StartCoroutine(AttackCouroutine());
                }
            }
        }

    }

    IEnumerator AttackCouroutine()
    {
        isAttack = true;
        currentArms.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentArms.attackDelayA);
        isSwing = true;

        // 공격 활성화 시점
        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentArms.attackDelayB);
        isSwing = false;

        yield return new WaitForSeconds(currentArms.attackDelay - currentArms.attackDelayA - currentArms.attackDelayB);

        isAttack = false;
    }
    protected abstract IEnumerator HitCoroutine();


    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, currentArms.range))
        {
            return true;
        }

        return false;
    }

    public virtual void ArmsChange(Arms _arms)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }

        currentArms = _arms;
        WeaponManager.currentWeapon = currentArms.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentArms.anim;

        currentArms.transform.localPosition = Vector3.zero;
        currentArms.gameObject.SetActive(true);
    }
}
