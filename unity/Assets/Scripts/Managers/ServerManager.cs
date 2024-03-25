using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : MonoBehaviour
{

    public GameObject player;
    public Transform playerTransform;

    private UnityWebRequest www;
    private Coroutine requestCoroutine;

    private string url = "http://localhost:8080/api";

    // Start is called before the first frame update
    void Start()
    {
        // Player를 찾아서 할당
        StartCoroutine(FindPlayerDelayed());

        // 스프링부트 요청
        //requestCoroutine = StartCoroutine(SendGetRequest());
    }

    void Update() {
        if (player.activeInHierarchy && requestCoroutine == null) {
            // Player 게임 오브젝트가 활성화되고 코루틴이 실행 중이지 않으면 코루틴을 시작
            //requestCoroutine = StartCoroutine(SendGetRequest());
            requestCoroutine = StartCoroutine(SendPostRequest());
        }
        else if (!player.activeInHierarchy && requestCoroutine != null) {
            // Player 게임 오브젝트가 비활성화되고 코루틴이 실행 중이면 코루틴을 중단
            StopCoroutine(requestCoroutine);
            requestCoroutine = null;

            if (www != null) {
                www.Dispose(); // UnityWebRequest 인스턴스를 정리
            }
        }
    }

    private void OnDisable() {
        Debug.Log("OnDisable 진입");
        if(requestCoroutine != null) {
            StopCoroutine(requestCoroutine);
        }

        if(www != null) {
            www.Dispose();  // UnityWebRequest 인스턴스 정리
        }
    }

    private void OnEnable() {
        Debug.Log("OnEnable 진입");
        //requestCoroutine = StartCoroutine(SendGetRequest());
        requestCoroutine = StartCoroutine(SendPostRequest());
    }
    

    private void FindPlayer() {
        // Player를 찾아서 할당
        player = GameObject.Find("Player1(Clone)");

        if (player == null) {
            Debug.LogError("Player1(Clone) not found!");
        }
        else {
            playerTransform = player.transform; // 플레이어를 찾았으면 transform 할당
        }
    }

    IEnumerator FindPlayerDelayed() {
        yield return new WaitForSeconds(1f); // 1초 대기

        FindPlayer(); // 플레이어를 찾는 메서드 호출
    }

    IEnumerator SendGetRequest() {

        while(true) {

            // Player 게임 오브젝트 비활성화 시 코루틴 중단
            if(!player.activeInHierarchy) {
                yield break;
            }

            Debug.Log("LoadData 진입");
            www = UnityWebRequest.Get(url + "/test/get");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log("웹 리퀘스트 에러");
                Debug.Log(www.error);
            }
            else {
                Debug.Log("웹 리퀘스트 성공");
                string data = www.downloadHandler.text;
                Debug.Log(data);
            }

            if (www != null) {
                www.Dispose();  // UnityWebRequest 인스턴스 정리
            }

            yield return new WaitForSeconds(0.06f);
        }
        
    }

    IEnumerator SendPostRequest() {

        WWWForm form;

        while(true) {

            // Player 게임 오브젝트 비활성화 시 코루틴 중단
            if (!player.activeInHierarchy) {
                yield break;
            }

            form = new WWWForm();

            form.AddField("posX", playerTransform.position.x.ToString());
            form.AddField("posY", playerTransform.position.y.ToString());
            form.AddField("posZ", playerTransform.position.z.ToString());
            www = UnityWebRequest.Post(url + "/test/post", form);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log("웹 리퀘스트 에러");
                Debug.Log(www.error);
            }
            else {
                Debug.Log("웹 리퀘스트 성공");
                string data = www.downloadHandler.text;
                Debug.Log(data);
            }

            if (www != null) {
                www.Dispose();  // UnityWebRequest 인스턴스 정리
            }

            yield return new WaitForSeconds(0.06f);
        }

    }
}
