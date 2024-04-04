using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject otherPlayerPrefab;
    public Transform[] spawnPoints;

    private int myIdx;

    void Start()
    {
        checkLoginUser();

        SpawnPlayer();
    }

    // 유저 스폰하기
    void SpawnPlayer()
    {
        if(myIdx == -1)
        {
            return;
        }

        // 데이터매니저에 플레이어 게임오브젝트 넣기
        DataManager.Instance.players.Clear();
        // 데이터매니저에 플레이어 팔 매니저 넣기
        //DataManager.Instance.weaponManagerScript.Clear();
        for(int i = 0; i < DataManager.Instance.cnt; i++)
        {
            GameObject player;
            if(myIdx == i)
            {
                player = Instantiate(playerPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                player.transform.Find("Main Camera/Holder").GetComponent<ArmsControl>().isMine = true;
                player.transform.Find("Main Camera/Holder").GetComponent<AxeController>().isMine = true;
                player.transform.Find("Main Camera/Holder").GetComponent<PickAxeController>().isMine = true;
            }
            else
            {
                player = Instantiate(otherPlayerPrefab, spawnPoints[i].position, spawnPoints[i].rotation);
                player.transform.Find("HPCanvas/PlayerNameText").GetComponent<TMPro.TMP_Text>().text = DataManager.Instance.sessionList[i].UserName;
            }
            DataManager.Instance.players.Add(player);
            //DataManager.Instance.weaponManagerScript.Add(player.transform.Find("Main Camera/Holder").GetComponent<WeaponManager>());
        }


        //int spawnIndex = Random.Range(0, spawnPoints.Length);
        //Transform spawnPoint = spawnPoints[spawnIndex];

        //GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);


    }

    // 유저 목록 중 로그인 한 유저 인덱스 찾기
    void checkLoginUser()
    {
        for(int i = 0; i < DataManager.Instance.cnt; i++)
        {
            // 세션 목록 중 로그인 한 유저와 id가 같으면
            if(DataManager.Instance.sessionList[i].UserId == DataManager.Instance.loginUserInfo.UserId)
            {
                myIdx = i;
                DataManager.Instance.myIdx = myIdx;
                return;
            }
        }

        myIdx = -1;
    }
}
