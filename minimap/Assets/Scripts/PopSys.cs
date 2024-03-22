using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PopSys : MonoBehaviour
{
    public GameObject popup;
    Animator anim;
    public static PopSys Instance { get; private set; }

    Action onClickOkay, onClickCancel;

    private void Awake()
    {
        Instance = this;
        anim = popup.GetComponent<Animator>();
    }

    public void OpenPopUp(
        Action onClickOkay,
        Action onClickCancel)
    {
        this.onClickOkay = onClickOkay;
        this.onClickCancel = onClickCancel ;
        popup.SetActive(true);

    }


    public void OnClickOkay()
    {
        if (onClickOkay != null)
        {
            onClickOkay();
        }
        ClosePopup();
    }

    public void OnClickCancel()
    {
        if (onClickCancel != null)
        {
            onClickCancel();
        }
        ClosePopup();
    }

    void ClosePopup()
    {
        Debug.Log("이거는 popup값" + popup);
        Debug.Log("이거는 popup 오브젝트 값"+popup.gameObject);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        Debug.Log("Current Animator State: " + stateInfo.fullPathHash);
        anim.SetTrigger("close");
        popup.SetActive(false);

    }

 

}
