using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ResponseData
{
    public string email;
    public string name;
    public string password;
    public string profileImg;
    public int rating;
    public int userId;
}

[System.Serializable]
public class ReturnValue
{
    public ResponseData[] data;
}

public class Rank : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rankScore;
    public TextMeshProUGUI winLose;

    public void Start()
    {
        StartCoroutine(GetRank());
    }

    IEnumerator GetRank()
    {
        // 서버에 요청을 보낼 URL 설정
        string url = "http://192.168.100.146:8080/user/all";
        int recieveNum = 15;

        // UnityWebRequest 객체 생성
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // 요청 보내기
            yield return www.SendWebRequest();

            // 에러 체크
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // 성공적으로 요청이 완료된 경우 응답 받아오기
                string jsonResponse = www.downloadHandler.text;
                jsonResponse = "{\"data\":" + jsonResponse + "}";
                ReturnValue returnValue = JsonUtility.FromJson<ReturnValue>(jsonResponse);
                ResponseData[] sortedData = returnValue.data.OrderByDescending(x => x.rating).ToArray();

                // 데이터 출력
                for (int i = 0; i < sortedData.Length; i++)
                {
                    if (sortedData[i].userId == recieveNum)
                    {
                        Debug.Log(sortedData[i].name);
                        Debug.Log(sortedData[i].rating);
                        nameText.text = sortedData[i].name;
                        rankScore.text = sortedData[i].rating.ToString();
                        Debug.Log(nameText.text);
                        Debug.Log(rankScore.text);
                        break;
                    }

                    //if ( )
                }
            }
        }
    }
}
