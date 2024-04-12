using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [Header("Commons")]
    public GameObject LoginScene;
    public GameObject LobbyScene;
    public LoginManager loginManagerScript; // �α��� �Ŵ��� ��ũ��Ʈ
    public TCPConnectManager TCPConnectManagerScript;   // TCP ��� ���� ��Ʈ��Ʈ

    [Header("Lobby")]
    public TMP_Text LobbyMyName;    // ������ �̸�
    public GameObject UserListElement;  // ���� ����Ʈ�� �� ���� ������Ʈ
    public Button LobbyAllListBtn;  // ��� ���� ����Ʈ ���� ��ư
    public Button LobbyFriendListBtn;   // ģ�� ����Ʈ ���� ��ư
    public GameObject ScrollViewLobbyList;  // ��� ���� ����Ʈ ���� ��ũ�Ѻ�
    public GameObject ScrollViewFriendList; // ģ�� ����Ʈ ���� ��ũ�Ѻ�
    public GameObject UserInfoDetail;   // ���� ����Ʈ���� ������ �����ϴ� ���� ������ â
    public TMP_Text ConnectedUserCount; // ���� ���� ī��Ʈ
    public GameObject CreateChannel;    // �� ����� �˾�

    [Header("Channel")]



    private string url = "https://j10c211.p.ssafy.io:8080";   // ��û URL

    private void OnEnable()
    {
        //    // �α��� �� ���� ���� ��쿡�� �������� ����(�α��� �� �� Login���� ���� ������ ���� ����)
        //    if(DataManager.Instance.loginUserInfo.UserId != 0)
        //    {
        //        TCPConnectManagerScript.gameObject.SetActive(false);
        //        LobbyInit();
        //        TCPConnectManagerScript.gameObject.SetActive(true);
        //    }
        LobbyInit();
    }

    private void OnDisable()
    {
        // ���� ���� ����Ʈ �����
        //Transform content = ScrollViewLobbyList.transform.Find("ScrollView/Viewport/Content");
        //List<GameObject> children = new List<GameObject>();
        //foreach(Transform child in content)
        //{
        //    children.Add(child.gameObject);
        //}
        //foreach(GameObject child in children)
        //{
        //    Destroy(child);
        //} 

    }

    public void test()
    {
        Debug.Log("asdf");
    }

    /* ======================== �κ� ======================== */
    // �κ� ���� �� ������ �ʱ�ȭ(����� ���� ��)
    public void LobbyInit()
    {
        //Debug.Log("LobbyInit");
        string name = DataManager.Instance.loginUserInfo.Name;    // chk
        int id = DataManager.Instance.loginUserInfo.UserId;   // chk

        // ������
        LobbyMyName.text = name + " #" + id;

        // TCP���� ����
        //TCPConnectManager.Instance.gameObject.SetActive(true); // chk
        //TCPConnectManagerScript.OnApplicationQuit();
        TCPConnectManager.Instance.ConnectToServer();   // chk
        // ģ�� ����
        setFriendUsers();
    }

    // �κ� ������(�α��� ȭ������)
    public void LobbyOut()
    {
        LobbyScene.SetActive(false);
        //TCPConnectManager.Instance.gameObject.SetActive(false); // chk
        TCPConnectManager.Instance.OnApplicationQuit();
        LoginScene.SetActive(true);
        loginManagerScript.LoginBtn.interactable = true;
        DataManager.Instance.dataClear();
        gameObject.SetActive(false);
    }

    // ��ü ���� �ҷ�����
    public void setAllUsers(string response)
    {
        // ���� ������ ���� ������ �ҷ�����
        User[] userList = TCPConnectManager.Instance.getConnectedUsers(response);   // chk

        if(userList != null)
        {
            // ���� ����Ʈ �� ��ũ�Ѻ�(������ �θ�)
            Transform content = ScrollViewLobbyList.transform.Find("ScrollView/Viewport/Content");
            // ���� ����Ʈ ����
            // TODO: ������Ʈ Ǯ��
            foreach(Transform child in content) {
                Destroy(child.gameObject);
            }

            // ���� ����Ʈ�� �� ������ ������Ʈ ����
            GameObject[] userListElements = new GameObject[userList.Length];

            // ���� ����Ʈ ������ ������Ʈ�� �� ������
            //GameObject[] activeUserData = new GameObject[userList.Length];
            for (int i = 0; i < userList.Length; i++)
            {

                // �ڱ� �ڽ��� �ǳʶٱ�
                if(userList[i].UserId == DataManager.Instance.loginUserInfo.UserId)   // chk
                {
                    continue;
                }

                // ��� ���� �� �θ� ����
                userListElements[i] = Instantiate(UserListElement);
                userListElements[i].transform.SetParent(content, false);
                // ���� ������ �ʱ�ȭ
                UserListElement userListElementScript = userListElements[i].GetComponent<UserListElement>();
                userListElementScript.Id = userList[i].UserId;
                userListElementScript.Email = userList[i].Email;
                userListElementScript.Name = userList[i].Name;
                userListElementScript.UserInfoDetail = UserInfoDetail;
          
                // ������ ���̱�
                userListElementScript.UserNameText.text = userList[i].Name + " #" + userList[i].UserId;
            }

            // ���� ���� �ο� �ؽ�Ʈ ������Ʈ
            ConnectedUserCount.text = "���� �ο�: " + userList.Length;
        }
    }

    // ģ�� ����Ʈ �ҷ�����
    private void setFriendUsers()
    {
        //Debug.Log("GetFriends");

        // ���� ģ�� ���� ������ �ҷ�����
        StartCoroutine(getFriendList((Friendship[] userList) =>
        {
            // ģ���� ������
            if (userList != null)
            {
                // ģ�� ����Ʈ �� ��ũ�Ѻ�(������ �θ�)
                Transform content = ScrollViewFriendList.transform.Find("ScrollView/Viewport/Content");

                // ���� ����Ʈ ����
                // TODO: ������Ʈ Ǯ��
                foreach (Transform child in content)
                {
                    Destroy(child.gameObject);
                }

                // ģ�� ����Ʈ�� �� ������ ������Ʈ ����
                GameObject[] userListElements = new GameObject[userList.Length];

                // ���� ����Ʈ ������ ������Ʈ�� �� ������
                //GameObject[] activeUserData = new GameObject[userList.Length];
                for (int i = 0; i < userList.Length; i++)
                {
                    // ��� ���� �� �θ� ����
                    userListElements[i] = Instantiate(UserListElement);
                    userListElements[i].transform.SetParent(content, false);
                    
                    // ���� ������ �ʱ�ȭ
                    UserListElement userListElementScript = userListElements[i].GetComponent<UserListElement>();
                    userListElementScript.Id = userList[i].FriendId;
                    userListElementScript.Name = userList[i].FriendName;
                    userListElementScript.UserInfoDetail = UserInfoDetail;

                    // ������ ������
                    string showData = userList[i].FriendName + " #" + userList[i].FriendId;
                    //Debug.Log(showData);

                    // ������ ���� ��û ���� ���� ���� ���
                    if (userList[i].IsWaiting && !userList[i].IsFrom)
                    {
                        userListElements[i].transform.Find("Border").GetComponent<UnityEngine.UI.Outline>().effectColor = Color.red;
                        // ������ ���̱�
                        showData += "  (ģ����û)";
                    }
                    // ���� ���� ��û ���� ���� ���� ���
                    else if(userList[i].IsWaiting && userList[i].IsFrom)
                    {
                        userListElements[i].transform.Find("Border").GetComponent<UnityEngine.UI.Outline>().effectColor = Color.green;
                        // ������ ���̱�
                        showData += "  (�������)";
                    }

                    // ������ ���̱�
                    userListElementScript.UserNameText.text = showData;
                }
            }
        }));
    }

    // ��ü ���� ��� / ģ�� ��� ��ư Ŭ�� ��
    public void clickUserListBtn()
    {
        LobbyAllListBtn.interactable = !LobbyAllListBtn.IsInteractable();
        LobbyFriendListBtn.interactable = !LobbyFriendListBtn.IsInteractable();

        ScrollViewLobbyList.SetActive(!ScrollViewLobbyList.activeSelf);
        ScrollViewFriendList.SetActive(!ScrollViewFriendList.activeSelf);

        setFriendUsers();
    }

    // ģ�� ��� �ҷ�����
    private IEnumerator getFriendList(System.Action<Friendship[]> callback)
    {
        int userId = DataManager.Instance.loginUserInfo.UserId;   // chk

        string requestUrl = url + "/friend/list?userId=" + userId ;

        using (UnityWebRequest friendCheckRequest = UnityWebRequest.Get(requestUrl))
        {
            friendCheckRequest.SetRequestHeader("Authorization", DataManager.Instance.accessToken);   // chk
            yield return friendCheckRequest.SendWebRequest();

            // ��û ���� ��
            if (friendCheckRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(friendCheckRequest.error);
                yield break;
            }
            // ��û ���� ��
            else
            {
                string result = friendCheckRequest.downloadHandler.text;

                Friendship[] friendList = JsonArrParser.FromJson<Friendship>(result);
            
                callback(friendList);
            }
        }
    }

    // ģ������, ��������� ���� Ȯ��
    public IEnumerator FriendCheckRequest(int friendId, System.Action<string> callback)
    {
        int userId = DataManager.Instance.loginUserInfo.UserId;   // chk

        string requestUrl = url + "/friend/profile?userId=" + userId + "&friendId=" + friendId;

        using (UnityWebRequest friendCheckRequest = UnityWebRequest.Get(requestUrl))
        {
            friendCheckRequest.SetRequestHeader("Authorization", DataManager.Instance.accessToken);   // chk
            yield return friendCheckRequest.SendWebRequest();

            // ��û ���� ��
            if(friendCheckRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(friendCheckRequest.error);
                yield break;
            }
            // ��û ���� ��
            else
            {
                string result = friendCheckRequest.downloadHandler.text;
                //Debug.Log(result);
                callback(result);
            }
        }
    }

    // ģ�� �߰� ��ư Ŭ�� ��
    public IEnumerator addFriend(int friendId)
    {
        Debug.Log("addFriend");
        int userId = DataManager.Instance.loginUserInfo.UserId;   // chk

        string requestUrl = url + "/friend/add";

        string jsonRequestBody =    "{" +
                                        "\"userId\": " + userId + "," +
                                        "\"friendId\": " + friendId + 
                                    "}";

        //Debug.Log(jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // ��û ����
        using (UnityWebRequest addFriendRequest = new UnityWebRequest(requestUrl, "POST"))
        {
            addFriendRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            addFriendRequest.downloadHandler = new DownloadHandlerBuffer();
            addFriendRequest.SetRequestHeader("Content-Type", "application/json");
            addFriendRequest.SetRequestHeader("Authorization", DataManager.Instance.accessToken); // chk

            yield return addFriendRequest.SendWebRequest();

            // ��û ���� ��
            if (addFriendRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(addFriendRequest.error);
                yield break;
            }
            // ��û ���� ��
            else
            {
                setFriendUsers();
                //string result = addFriendRequest.downloadHandler.text;
                //Debug.Log(result);
            }
        }
    }

    // ���� ��ư Ŭ�� ��
    public IEnumerator acceptFriend(int friendId)
    {
        int userId = DataManager.Instance.loginUserInfo.UserId;   // chk

        string requestUrl = url + "/friend/accept";

        // DB���� ���� ��û�� ���: userId, �޴� ���: friendId��
        // �� �������� ���� �� �����ϴ� ���(�α��� �� ���)�� friendId�̰�, �α��� �� ������ userId�� ��������Ƿ�
        // ���� �ٲ㼭 ��û ������ �Ѵ�.
        string jsonRequestBody =    "{" +
                                        "\"userId\": " + friendId + "," +
                                        "\"friendId\": " + userId +
                                    "}";

        //Debug.Log(jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // ��û ����
        using (UnityWebRequest acceptFriendRequest = new UnityWebRequest(requestUrl, "POST"))
        {
            acceptFriendRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            acceptFriendRequest.downloadHandler = new DownloadHandlerBuffer();
            acceptFriendRequest.SetRequestHeader("Content-Type", "application/json");
            acceptFriendRequest.SetRequestHeader("Authorization", DataManager.Instance.accessToken);  // chk

            yield return acceptFriendRequest.SendWebRequest();

            // ��û ���� ��
            if (acceptFriendRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(acceptFriendRequest.error);
                yield break;
            }

            setFriendUsers();
        }
    }


    // =================================���͹�
    // �� ����� ��ư Ŭ�� ��
    public void CreateChannelBtnClicked()
    {
        CreateChannel.SetActive(true);
    }

    // �� ������ư Ŭ�� ��
    public void JoinChannelBtnClicked()
    {
        TCPConnectManager.Instance.joinChannel();   // chk
    }
}
