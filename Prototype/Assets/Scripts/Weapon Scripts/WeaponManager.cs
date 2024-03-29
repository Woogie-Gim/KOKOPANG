using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // 무기 중복 교체 실행 방지
    public static bool isChangeWeapon = false;

    // 현재 무기와 현재 무기의 애니메이션
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;

    // 현재 무기의 타입
    [SerializeField]
    private string currentWeaponType;

    // 무기 교체 딜레이
    [SerializeField]
    private float changeWeaponDelayTime;
    // 무기 교체가 완전히 끝난 시점
    [SerializeField]
    private float changeWeaponEndDelayTime;

    // 무기 종류들 전부 관리
    [SerializeField]
    private Arms[] arms;
    [SerializeField]
    private Arms[] axes;
    [SerializeField]
    private Arms[] pickaxes;

    // 관리 차원에서 쉽게 무기 접근이 가능하도록 만듦
    private Dictionary<string, Arms> armsDictionary = new Dictionary<string, Arms>();
    private Dictionary<string, Arms> axeDictionary = new Dictionary<string, Arms>();
    private Dictionary<string, Arms> pickaxeDictionary = new Dictionary<string, Arms>();

    [SerializeField]
    private ArmsControl theArmsControl;
    [SerializeField]
    private AxeController theAxeContorller;
    [SerializeField]
    private PickAxeController thePickAxeContorller;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < arms.Length; i++) 
        {
            armsDictionary.Add(arms[i].armsName, arms[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDictionary.Add(axes[i].armsName, axes[i]);
        }
        for (int i = 0; i < pickaxes.Length; i++)
        {
            pickaxeDictionary.Add(pickaxes[i].armsName, pickaxes[i]);
        }
    }

    public IEnumerator ChangeWeaponCoroutine(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_Out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    private void CancelPreWeaponAction()
    {
        switch (currentWeaponType)
        {
            case "ARMS":
                ArmsControl.isActivate = false;
                if (QuickSlotController.go_HandItem != null)
                {
                    Destroy(QuickSlotController.go_HandItem);
                }
                break;
            case "AXE":
                AxeController.isActivate = false;
                break;
            case "PICKAXE":
                PickAxeController.isActivate = false;
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if (_type == "ARMS")
        {
            theArmsControl.ArmsChange(armsDictionary[_name]);
        }
        else if (_type == "AXE")
        {
            theAxeContorller.ArmsChange(axeDictionary[_name]);
        }
        else if (_type == "PICKAXE")
        {
            thePickAxeContorller.ArmsChange(pickaxeDictionary[_name]);
        }
    }
}
