using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header ("Commons")]
    public GameObject Login;    // 로그인 오브젝트
    public GameObject SignUpPopUp;  // 회원가입 팝업 오브젝트
    public GameObject Lobby; // 로비 오브젝트
    public LobbyManager LobbyManagerScript; // 로비매니저 스크립트

    [Header("Login")]
    public TMP_InputField LoginEmail;   // 로그인 이메일 인풋필드
    public TMP_InputField LoginPassword;    // 패스워드 인풋필드
    public Button LoginSignUpBtn;   // 회원가입 팝업 띄울 버튼
    public Button LoginBtn; // 로그인 버튼
    public Button LoginExit;    // 종료 버튼

    [Header ("SignUp")]
    public TMP_InputField SignUpEmail;  // 회원가입 이메일 인풋필드
    public TMP_InputField SignUpName;   // 회원가입 이름 인풋필드
    public TMP_InputField SignUpPassword;   // 회원가입 비밀번호 인풋필드
    public TMP_InputField SignUpPasswordCheck;  // 회원가입 비밀번호 재입력 필드
    public TMP_Text SignUpFeedback; // 회원가입 성공/실패 여부 피드백 텍스트
    public Button SignupConfirm;    // 회원가입 하기 버튼
    public Button SignupCancel; // 회원가입 종료 버튼

    [Header ("LoginUserInfo")]
    public string accessToken; // access token
    public string refreshToken;    // refresh token
    public User loginUserInfo;

    private string url = "http://j10c211.p.ssafy.io:8080";   // 요청 URL


    /* ======================== 회원가입 ======================== */
    // 회원가입으로 가기
    public void GoSignUp() 
    {
        SignUpPopUp.SetActive(true);
        SignupConfirm.interactable = true;
    }

    // 회원가입 버튼 클릭 시
    public void SignUpConfirm() 
    {
        string email = SignUpEmail.text;
        string name = SignUpName.text;
        string password = SignUpPassword.text;
        string passwordCheck = SignUpPasswordCheck.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name) ||
            string.IsNullOrEmpty(password) || string.IsNullOrEmpty(passwordCheck)) {
            SignUpFeedback.text = "모든 필드를 입력해주세요.";
            return;
        }
        else if (password != passwordCheck) {
            SignUpFeedback.text = "비밀번호가 일치하지 않습니다.";
            return;
        }
        else {
            SignUpFeedback.text = "";
        }

        SignupConfirm.interactable = false;
        // 회원가입 요청 보내기
        StartCoroutine(SignUpRequest(email, password, name));
    }

    // 회원가입 취소 시
    public void SignUpCancel() 
    {
        // 회원가입 취소 시 입력 필드 및 피드백 텍스트 초기화
        SignUpEmail.text = "";
        SignUpName.text = "";
        SignUpPassword.text = "";
        SignUpPasswordCheck.text = "";
        SignUpFeedback.text = "";

        // 로그인 페이지로 이동
        SignUpPopUp.SetActive(false);
        //Login.SetActive(true);
    }

    // 회원가입 요청 보내기
    private IEnumerator SignUpRequest(string email, string password, string name)
    {
        string requestUrl = url + "/user/signup";
        User user = new User
        {
            Email = email,
            Password = password,
            Name = name
        };
        string jsonRequestBody = JsonUtility.ToJson(user);
        //Debug.Log("Request Body: " + jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // 요청 생성
        using (UnityWebRequest request = new UnityWebRequest(requestUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            // 요청 성공 시
            if (request.result == UnityWebRequest.Result.Success)
            {
                SignUpFeedback.text = "회원가입이 완료되었습니다.";
            }
            // 요청 실패 시
            else
            {
                Debug.LogError("Error: " + request.error);
                // 아이디 중복
                if (request.responseCode == 409)
                {
                    SignUpFeedback.text = "중복된 아이디입니다.";
                }
                // 그 외
                else
                {
                    SignUpFeedback.text = "서버에 연결할 수 없습니다.";
                }
                //string responseBody = request.downloadHandler.text;

                SignupConfirm.interactable = true;
                yield break;
            }

            // 로그인 페이지로 이동
            //SignUpCancel();
        }
    }

    /* ======================== 로그인 ======================== */
    // 로그인버튼 클릭 시
    public void LoginConfirm()
    {
        string email = LoginEmail.text;
        string password = LoginPassword.text;

        // 입력 확인
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return;
        }

        LoginBtn.interactable = false;
        StartCoroutine(LoginRequest(email, password));
    }

    // 로그인 요청 보내기
    private IEnumerator LoginRequest(string email, string password)
    {
        string requestUrl = url + "/login";

        WWWForm form = new WWWForm();
        form.AddField("username", email);
        form.AddField("password", password);

        // 요청 생성
        using (UnityWebRequest request = UnityWebRequest.Post(requestUrl, form))
        {
            yield return request.SendWebRequest();

            // 요청 성공 시
            if (request.result == UnityWebRequest.Result.Success)
            {
                //Debug.Log("로그인 성공");
                accessToken = request.GetResponseHeader("Authorization");
                refreshToken = request.GetResponseHeader("refreshToken");
                //Debug.Log("accessToken: " + accessToken);
                //Debug.Log("refreshToken: " + refreshToken);

                // 로그인 한 유저 정보 불러오기
                using (UnityWebRequest userProfileRequest = UnityWebRequest.Get(url + "/user/profile?email=" + email))
                {
                    userProfileRequest.SetRequestHeader("Authorization", accessToken);
                    yield return userProfileRequest.SendWebRequest();

                    // 정보 요청 실패 시
                    if(userProfileRequest.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(userProfileRequest.error);
                    }
                    // 정보 요청 성공 시
                    else
                    {
                        //Debug.Log(userProfileRequest.downloadHandler.text);
                        loginUserInfo = JsonUtility.FromJson<User>(userProfileRequest.downloadHandler.text);

                        // 로비 데이터 초기화 호출
                        LobbyManagerScript.LobbyInit(loginUserInfo.Name, loginUserInfo.Id);
                        //Debug.Log(userProfile.Id);
                    }
                }

                Login.SetActive(false);
                Lobby.SetActive(true);
            }
            // 요청 실패 시
            else
            {
                Debug.LogError("Error: " + request.error);
                // 아이디 중복
                if (request.responseCode == 400)
                {
                    Debug.Log("로그인 실패");
                }
                // 그 외
                else
                {
                    Debug.Log("서버에 연결할 수 없습니다.");
                }

                LoginBtn.interactable = true;
                yield break;
            }

        }
    }

}
