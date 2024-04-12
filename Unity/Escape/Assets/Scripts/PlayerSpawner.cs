using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject otherPlayerPrefab;
    public Transform[] spawnPoints;

    public GameObject PlayerUser;

    private int myIdx;

    void Start()
    {
        checkLoginUser();

        SpawnPlayer();

        //ChatUISetting();
    }

    // 유저 스폰하기
    void SpawnPlayer()
    {
        if (myIdx == -1)
        {
            return;
        }

        // 데이터매니저에 플레이어 게임오브젝트 넣기
        DataManager.Instance.players.Clear();
        // 데이터매니저에 플레이어 팔 매니저 넣기
        //DataManager.Instance.weaponManagerScript.Clear();
        for (int i = 0; i < DataManager.Instance.cnt; i++)
        {
            if (myIdx == i)
            {
                PlayerUser = Instantiate(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                //PlayerUser.transform.Find("Main Camera/Holder").GetComponent<ArmsControl>().isMine = true;
                //PlayerUser.transform.Find("Main Camera/Holder").GetComponent<AxeController>().isMine = true;
                //PlayerUser.transform.Find("Main Camera/Holder").GetComponent<PickAxeController>().isMine = true;
            }
            else
            {
                PlayerUser = Instantiate(otherPlayerPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                PlayerUser.transform.Find("HPCanvas/PlayerNameText").GetComponent<TMPro.TMP_Text>().text = DataManager.Instance.sessionList[i].UserName;
            }
            DataManager.Instance.players.Add(PlayerUser);
            //DataManager.Instance.weaponManagerScript.Add(player.transform.Find("Main Camera/Holder").GetComponent<WeaponManager>());
        }


        //int spawnIndex = Random.Range(0, spawnPoints.Length);
        //Transform spawnPoint = spawnPoints[spawnIndex];

        //GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // 유저 채팅 UI 설정하기
        GameObject Chatting = DataManager.Instance.players[myIdx].transform.Find("Canvas/Chatting").gameObject;
        GameObject ChatScrollView = Chatting.transform.Find("ScrollViewChat").gameObject;
        TMPro.TMP_InputField inputField = Chatting.transform.Find("Input/InputChat").GetComponent<TMPro.TMP_InputField>();

        //TCPConnectManager.Instance.IngameChatUI = Chatting;
        TCPConnectManager.Instance.InGameChattingList = ChatScrollView;
        TCPConnectManager.Instance.InGameChat = inputField;


    }

    // 유저 목록 중 로그인 한 유저 인덱스 찾기
    void checkLoginUser()
    {
        for (int i = 0; i < DataManager.Instance.cnt; i++)
        {
            // 세션 목록 중 로그인 한 유저와 id가 같으면
            if (DataManager.Instance.sessionList[i].UserId == DataManager.Instance.loginUserInfo.UserId)
            {
                myIdx = i;
                DataManager.Instance.myIdx = myIdx;
                return;
            }
        }

        myIdx = -1;
    }

    // 유저 채팅 UI 설정하기
    void ChatUISetting()
    {
        GameObject Chatting = PlayerUser.transform.Find("Canvas/Chatting").gameObject;
        GameObject ChatScrollView = Chatting.transform.Find("ScrollViewChat").gameObject;
        TMPro.TMP_InputField inputField = Chatting.transform.Find("Input/InputChat").GetComponent<TMPro.TMP_InputField>();

        //TCPConnectManager.Instance.IngameChatUI = Chatting;
        TCPConnectManager.Instance.InGameChattingList = ChatScrollView;
        TCPConnectManager.Instance.InGameChat = inputField;
    }
}
