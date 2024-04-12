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

    // ��� ����
    [SerializeField] private GameObject[] results;

    // ���� �̸�
    [SerializeField] private TMP_Text[] userNames;

    // ���� ���ھ�
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

    // ��ũ�� ��û ������
    private IEnumerator RankUpRequest(bool isEscape)
    {
        string url = "https://j10c211.p.ssafy.io:8080";   // ��û URL
        string requestUrl = url + "/user/rankup";

        string jsonRequestBody = "{" +
            $"\"userId\":\"{DataManager.Instance.loginUserInfo.UserId}\"," +
            $"\"score\":\"{DataManager.Instance.score[DataManager.Instance.myIdx]}\"," +
            $"\"isEscape\":\"{isEscape}\"" +
        "}";
        //Debug.Log("Request Body: " + jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // ��û ����
        using (UnityWebRequest request = new UnityWebRequest(requestUrl, "PUT"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", DataManager.Instance.accessToken);   // chk
            yield return request.SendWebRequest();

            // ��û ���� ��
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("��ũ�� ����");
            }
            // ��û ���� ��
            else
            {
                Debug.Log("��ũ�� ����");
                yield break;
            }
        }
    }
}
