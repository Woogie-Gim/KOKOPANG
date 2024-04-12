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
        // ������ ��û�� ���� URL ����
        string url = "http://192.168.100.146:8080/user/all";
        int recieveNum = 15;

        // UnityWebRequest ��ü ����
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            // ��û ������
            yield return www.SendWebRequest();

            // ���� üũ
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // ���������� ��û�� �Ϸ�� ��� ���� �޾ƿ���
                string jsonResponse = www.downloadHandler.text;
                jsonResponse = "{\"data\":" + jsonResponse + "}";
                ReturnValue returnValue = JsonUtility.FromJson<ReturnValue>(jsonResponse);
                ResponseData[] sortedData = returnValue.data.OrderByDescending(x => x.rating).ToArray();

                // ������ ���
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
