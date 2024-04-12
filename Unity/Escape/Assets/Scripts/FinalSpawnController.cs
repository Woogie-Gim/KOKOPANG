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
            // �ش� ������Ʈ�� ������ �װ��� ����Ͽ� ��ġ�� ����
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false; // ��� ��Ȱ��ȭ�Ͽ� ��ġ ����
                player.transform.position = finalSpawnPoint.position;
                cc.enabled = true; // �ٽ� Ȱ��ȭ
            }
            else
            {
                // CharacterController�� ���� ���, ���� ��ġ ����
                player.transform.position = finalSpawnPoint.position;
            }
        }

        finalBoarder.SetActive(true);
    }
}
