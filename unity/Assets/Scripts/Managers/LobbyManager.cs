using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [Header("Commons")]
    public LoginManager loginManagerScript; // 로그인 매니저 스크립트

    [Header("Lobby")]
    public TMP_Text LobbyMyName;    // 내정보 이름
    public GameObject UserListElement;  // 유저 리스트에 들어갈 유저 엘리먼트
    public Button LobbyAllListBtn;  // 모든 유저 리스트 보일 버튼
    public Button LobbyFriendListBtn;   // 친구 리스트 보일 버튼
    public GameObject ScrollViewLobbyList;  // 모든 유저 리스트 보일 스크롤뷰
    public GameObject ScrollViewFriendList; // 친구 리스트 보일 스크롤뷰
    public GameObject UserInfoDetail;   // 유저 리스트에서 누르면 등장하는 유저 상세정보 창


    private string url = "http://localhost:8080";   // 요청 URL


    /* ======================== 로비 ======================== */
    // 로비 입장 시 데이터 초기화(사용자 정보 등)
    public void LobbyInit(string name)
    {
        LobbyMyName.text = name;
        setAllUsers();
    }

    // 전체 유저 불러오기
    private void setAllUsers()
    {
        // 현재 접속한 유저 데이터 불러오기
        // TODO: 더미데이터 서버에서 받아온걸로 변경
        User[] userList = new User[3];
        userList[0] = new User
        {
            Id = 30,
            Email = "ww",
            Name = "더블유"
        };
        userList[1] = new User
        {
            Id = 31,
            Email = "ee",
            Name = "이이"
        };
        userList[2] = new User
        {
            Id = 29,
            Email = "qq",
            Name = "큐큐"
        };

        // 유저 리스트 들어갈 스크롤뷰(설정할 부모)
        Transform content = ScrollViewLobbyList.transform.Find("ScrollView/Viewport/Content");

        // 유저 리스트에 들어갈 각각의 컴포넌트 생성
        GameObject[] userListElements = new GameObject[userList.Length];

        // 유저 리스트 각각의 컴포넌트에 들어갈 데이터
        //GameObject[] activeUserData = new GameObject[userList.Length];
        for (int i = 0; i < userList.Length; i++)
        {
            // 요소 생성 및 부모 설정
            userListElements[i] = Instantiate(UserListElement);
            userListElements[i].transform.SetParent(content, false);
            // 유저 데이터 초기화
            UserListElement userListElementScript = userListElements[i].GetComponent<UserListElement>();
            userListElementScript.Id = userList[i].Id;
            userListElementScript.Email = userList[i].Email;
            userListElementScript.Name = userList[i].Name;
            userListElementScript.UserInfoDetail = UserInfoDetail;
            // 데이터 보이기
            userListElementScript.userNameText.text = userList[i].Name;
        }
    }

    // 친구 리스트 불러오기
    private void setFriendUsers()
    {

    }

    // 방 리스트 불러오기
    public void setChannelList()
    {

    }

    // 방 만들기
    public void createChannel()
    {

    }

    // 빠른입장
    public void quickEnter()
    {

    }

    // 참가
    public void participate()
    {

    }

    // 전체 유저 목록 / 친구 목록 버튼 클릭 시
    public void clickUserListBtn()
    {
        LobbyAllListBtn.interactable = !LobbyAllListBtn.IsInteractable();
        LobbyFriendListBtn.interactable = !LobbyFriendListBtn.IsInteractable();

        ScrollViewLobbyList.SetActive(!ScrollViewLobbyList.activeSelf);
        ScrollViewFriendList.SetActive(!ScrollViewFriendList.activeSelf);
    }

    // 친구인지, 대기중인지 여부 확인
    public IEnumerator FriendCheckRequest(int friendId, System.Action<string> callback)
    {
        int userId = loginManagerScript.loginUserInfo.Id;

        string requestUrl = url + "/friend/profile?userId=" + userId + "&friendId=" + friendId;

        using (UnityWebRequest friendCheckRequest = UnityWebRequest.Get(requestUrl))
        {
            friendCheckRequest.SetRequestHeader("Authorization", loginManagerScript.accessToken);
            yield return friendCheckRequest.SendWebRequest();

            // 요청 실패 시
            if(friendCheckRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(friendCheckRequest.error);
                yield break;
            }
            // 요청 성공 시
            else
            {
                string result = friendCheckRequest.downloadHandler.text;
                Debug.Log(result);
                callback(result);
            }
        }
    }

    // 친구 추가 버튼 클릭 시
    public IEnumerator addFriend(int friendId)
    {
        int userId = loginManagerScript.loginUserInfo.Id;

        string requestUrl = url + "/friend/add";

        string jsonRequestBody =    "{" +
                                        "\"userId\": " + userId + "," +
                                        "\"friendId\": " + friendId + 
                                    "}";

        Debug.Log(jsonRequestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);

        // 요청 생성
        using (UnityWebRequest addFriendRequest = new UnityWebRequest(requestUrl, "POST"))
        {
            addFriendRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            addFriendRequest.downloadHandler = new DownloadHandlerBuffer();
            addFriendRequest.SetRequestHeader("Content-Type", "application/json");
            addFriendRequest.SetRequestHeader("Authorization", loginManagerScript.accessToken);

            yield return addFriendRequest.SendWebRequest();

            // 요청 실패 시
            if (addFriendRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(addFriendRequest.error);
                yield break;
            }
            // 요청 성공 시
            else
            {
                string result = addFriendRequest.downloadHandler.text;
                Debug.Log(result);
            }
        }
    }

}
