using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserSignin : MonoBehaviour
{
    public string serverURL = "localhost/3000";
    public string username;
    public string password;

    public void Login()
    {
        StartCoroutine(SendLoginRequest());
    }

    IEnumerator SendLoginRequest()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest request = UnityWebRequest.Post(serverURL, form);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Login Successful");
        }
        else
        {
            Debug.Log("Login Failed");
        }
    }
}
