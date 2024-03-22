using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UserAuthenticator : MonoBehaviour
{
    public InputField IdInputField;
    public InputField IdCheckInputField;
    public InputField PWInputField;
    public InputField PWCheckInputField;
    public Text FeedbackText;
    public Button YesButton;
    public Button NoButton;
    public Button LoginButton;

    private string Url = "http://localhost:3000/";

    public void Start()
    {
        if (YesButton != null)
        {
            YesButton.onClick.AddListener(signUp);
        }
        if (NoButton != null)
        {
            NoButton.onClick.AddListener(CancelSignUp);
        }
        if (LoginButton != null)
        {
            LoginButton.onClick.AddListener(() => Login());
        }
    }

    public void signUp()
    {
        string id = IdInputField.text;
        string idCheck = IdCheckInputField.text;
        string password = PWInputField.text;
        string pwCheck = PWCheckInputField.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(idCheck) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(pwCheck))
        {
            FeedbackText.text = "모든 필드를 입력해주세요.";
            return;
        }

        if (id != idCheck)
        {
            FeedbackText.text = "ID가 일치하지 않습니다.";
            return;
        }

        if (password != pwCheck)
        {
            FeedbackText.text = "비밀번호가 일치하지 않습니다.";
            return;
        }

        StartCoroutine(Register(id, password));
    }

    public void CancelSignUp()
    {
        // 회원가입 취소 시 입력 필드 및 피드백 텍스트 초기화
        IdInputField.text = "";
        IdCheckInputField.text = "";
        PWInputField.text = "";
        PWCheckInputField.text = "";
        FeedbackText.text = "";
    }

    public IEnumerator Register(string Id, string password)
    {
        string url = Url + "register";
        string jsonRequestBody = "{\"Id\":\"" + Id + "\",\"password\":\"" + password + "\"}";
        UnityWebRequest request = UnityWebRequest.Post(url, jsonRequestBody);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
            FeedbackText.text = "서버에 연결할 수 없습니다.";
            yield break;
        }

        string responseBody = request.downloadHandler.text;
        Debug.Log("Response: " + responseBody);
        FeedbackText.text = "회원가입이 완료되었습니다.";
    }

    public void Login()
    {
        SceneManager.LoadScene("ChatRoom");
        StartCoroutine(LoginRequest(IdInputField.text, PWInputField.text));
    }

    private IEnumerator LoginRequest(string username, string password)
    {
        string url = Url + "login";
        string jsonRequestBody = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";
        UnityWebRequest request = UnityWebRequest.Post(url, jsonRequestBody);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
            FeedbackText.text = "서버에 연결할 수 없습니다.";
            yield break;
        }

        string responseBody = request.downloadHandler.text;
        Debug.Log("Response: " + responseBody);
        FeedbackText.text = "로그인 성공!";
    }

    public void LogOut()
    {
        StartCoroutine(LogoutRequest());
    }

    private IEnumerator LogoutRequest()
    {
        string url = Url + "logout";
        using (UnityWebRequest request = UnityWebRequest.Get(url)) // UnityWebRequest를 using 블록으로 감싸서 Dispose() 호출
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
                FeedbackText.text = "로그아웃 중 오류가 발생했습니다.";
                yield break;
            }

            PlayerPrefs.DeleteKey("AuthToken");
            FeedbackText.text = "로그아웃 되었습니다.";
        }
    }
}
