using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerUIController : MonoBehaviour
{
    //[SerializeField] private MenuController menuControllerScript;
    //[SerializeField] private CraftManual craftManualScript;
    //[SerializeField] private Inventory inventoryScript;

    [SerializeField] private GameObject GameMenuObj;
    [SerializeField] private GameObject CraftMenuObj;
    [SerializeField] private GameObject InventoryMenuObj;
    //[SerializeField] private GameObject Tooltip;
    [SerializeField] private GameObject TutorialObj;


    // Start is called before the first frame update
    void Start()
    {
        //GameMenu = menuControllerScript.transform.Find("MenuImage").gameObject;
        //CraftMenu = craftManualScript.transform.Find("Craft Manual Base").gameObject;
        //Inventory = inventoryScript.transform.Find("Inventory_Base").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // ���� �޴� �������� ��
            if (GameMenuObj.activeSelf)
            {
                // Ʃ�丮���� ����������
                if(TutorialObj.activeSelf)
                {
                    TutorialObj.SetActive(false);
                    MenuController.isTutorial = false;
                }
                else {
                    GameMenuObj.SetActive(false);
                    MenuController.isMenu = false;
                    MenuController.isTutorial = false;
                }
            }
            // ���� �޴� �������� ��
            else
            {
                if(InventoryMenuObj.activeSelf)   // �κ��丮 ���������� ����
                {
                    InventoryMenuObj.SetActive(false);
                    Inventory.inventoryActivated = false;
                }
                else if (CraftMenuObj.activeSelf) // ũ����Ʈ �޴� ���������� ����
                {
                    CraftMenuObj.SetActive(false);
                    CraftManual.isActivated = false;
                }
                else // �� �� �������� ��
                {
                    GameMenuObj.SetActive(true);
                    MenuController.isMenu = true;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            CraftManual.isActivated = !CraftMenuObj.activeSelf;
            CraftMenuObj.SetActive(!CraftMenuObj.activeSelf);
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            Inventory.inventoryActivated = !InventoryMenuObj.activeSelf;
            InventoryMenuObj.SetActive(!InventoryMenuObj.activeSelf);
        }
    }
}
