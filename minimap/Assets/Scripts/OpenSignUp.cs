using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSignUp : MonoBehaviour
{
   public void OnclickMyButton()
    {
        PopSys.Instance.OpenPopUp(
            () =>
            {
                Debug.Log("Onclick Okay");
            },
            () =>
            {
                Debug.Log("onclick?!?");
            }

            );
    }
}
