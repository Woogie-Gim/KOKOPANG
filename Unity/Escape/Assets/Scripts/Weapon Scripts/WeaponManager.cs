using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("MultiPlay")]
    public TCPConnectManager TCPConnectManagerScript;

    // ���� �ߺ� ��ü ���� ����
    public static bool isChangeWeapon = false;

    // ���� ����� ���� ������ �ִϸ��̼�
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;

    // ���� ������ Ÿ��
    public static string currentWeaponType;

    // ���� ��ü ������
    [SerializeField]
    private float changeWeaponDelayTime;
    // ���� ��ü�� ������ ���� ����
    [SerializeField]
    private float changeWeaponEndDelayTime;

    // ���� ������ ���� ����
    [SerializeField]
    private Arms[] arms;
    [SerializeField]
    private Arms[] axes;
    [SerializeField]
    private Arms[] pickaxes;

    // ���� �������� ���� ���� ������ �����ϵ��� ����
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

        //TCPConnectManagerScript = GameObject.Find("TCPConnectManager").GetComponent<TCPConnectManager>();
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

        // �ڱ��ڽ��� ���� �� ���� ��û ������
        //if (transform.IsChildOf(DataManager.Instance.players[DataManager.Instance.myIdx].transform))
        //{
        //    TCPConnectManagerScript.playerChangeArm(_type, _name);
        //}
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
