using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalSpawnController : MonoBehaviour
{
    public Transform finalSpawnPoint;
    public GameObject finalBoarder;

    private List<GameObject> players;

    void Start()
    {
        finalBoarder.SetActive(false);

        players = new List<GameObject>();
        StartCoroutine(AddPlayersCoroutine());
    }

    IEnumerator AddPlayersCoroutine()
    {
        yield return new WaitForSeconds(1f);

        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in allPlayers)
        {
            if (player.name == "Player(Clone)")
            {
                players.Add(player);
            }
        }
    }

    public void MovePlayersToFinalSpawn()
    {
        foreach (GameObject player in players)
        {
            // 해당 컴포넌트가 있으면 그것을 사용하여 위치를 설정
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false; // 잠시 비활성화하여 위치 설정
                player.transform.position = finalSpawnPoint.position;
                cc.enabled = true; // 다시 활성화
            }
            else
            {
                // CharacterController가 없는 경우, 직접 위치 설정
                player.transform.position = finalSpawnPoint.position;
            }
        }

        finalBoarder.SetActive(true);
    }
}
