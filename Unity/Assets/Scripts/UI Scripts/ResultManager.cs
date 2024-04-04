using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ResultManager : MonoBehaviour
{
    public static bool isResultManager;

    [SerializeField] private TMP_Text clearPlayerName;

    // 결과 정보
    [SerializeField] private GameObject[] results;

    // 유저 이름
    [SerializeField] private TMP_Text[] userNames;

    // 유저 스코어
    [SerializeField] private TMP_Text[] userScores;

    void Start()
    {
        for (int i = 0; i < results.Length; i++)
        {
            results[i].gameObject.SetActive(false);
        }

        int clearUserIdx = DataManager.Instance.getUserIndex(DataManager.Instance.clearUserId);
        for(int i = 0; i < DataManager.Instance.cnt; i++)
        {
            results[i].gameObject.SetActive(true);
            userNames[i].text = DataManager.Instance.sessionList[i].UserName;
            if(i == clearUserIdx)
            {
                userScores[i].text = (DataManager.Instance.score[i] - 100) + " + 100";
            }
            else
            {
                userScores[i].text = (DataManager.Instance.score[i]) + "";
            }
        }

        clearPlayerName.text = DataManager.Instance.sessionList[clearUserIdx].UserName;

        bool isEscape = clearUserIdx == DataManager.Instance.myIdx;
        StartCoroutine(RankUpRequest(isEscape));
    }

    public void GoToLobby()
    {
        isResultManager = false;
        TCPConnectManager.Instance.OnApplicationQuit();
        SceneManager.LoadScene("Login");
    }

    // 랭크업 요청 보내기
    private IEnumerator RankUpRequest(bool isEscape)
    {
        string url = "https://j10c211.p.ssafy.io:8080";   // 요청 URL
        string requestUrl = url + "/user/rankup";

        string jsonRequestBody = "{" +
            $"\"userId\":\"{DataManager.Instance.loginUserInfo.UserId}\"," +
            $"\"score\":\"{DataManager.Instance.score[DataManager.Instance.myIdx]}\"," +
            $"\"isEscape\":\"{isEscape}\"" +
        "}";
        //Debug.Log("Request Body: " + jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // 요청 생성
        using (UnityWebRequest request = new UnityWebRequest(requestUrl, "PUT"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", DataManager.Instance.accessToken);   // chk
            yield return request.SendWebRequest();

            // 요청 성공 시
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("랭크업 성공");
            }
            // 요청 실패 시
            else
            {
                Debug.Log("랭크업 실패");
                yield break;
            }
        }
    }
}
