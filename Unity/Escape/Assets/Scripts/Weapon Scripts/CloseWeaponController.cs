using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
{
    // ���� ������ Arms�� Ÿ�� ����
    [SerializeField]
    protected Arms currentArms;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip swingsound;

    // ���� �� ����
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;
    protected void TryAttack()
    {
        if (!Inventory.inventoryActivated && !CraftManual.isActivated && !FixTab.fixtabActivated)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!isAttack)
                {
                    // �ڷ�ƾ ����
                    StartCoroutine(AttackCouroutine());
                }
            }
        }

    }

    IEnumerator AttackCouroutine()
    {
        isAttack = true;
        audioSource.clip = swingsound;
        audioSource.Play();
        currentArms.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(currentArms.attackDelayA);
        isSwing = true;

        // ���� Ȱ��ȭ ����
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
