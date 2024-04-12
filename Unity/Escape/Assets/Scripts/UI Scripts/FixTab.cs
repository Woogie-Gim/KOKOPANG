using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FixTab : MonoBehaviour
{
    public static bool fixtabActivated = false;

    [SerializeField]
    private GameObject go_FixTabBase;

    public static void TryOpenFixTab()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            fixtabActivated = !fixtabActivated;

            if (fixtabActivated)
            {
                OpenFixTab();
            }
            else
            {
                CloseFixTab(); 
            }
        }
    }

    public static void OpenFixTab()
    {
        if (instance != null)
            instance.go_FixTabBase.SetActive(true);
    }

    public static void CloseFixTab()
    {
        if (instance != null) 
            instance.go_FixTabBase.SetActive(false);
    }

    private static FixTab instance;

    private void Awake()
    {
        instance = this;
    }
}

