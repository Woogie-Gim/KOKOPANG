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
        // Player�� ã�Ƽ� �Ҵ�
        StartCoroutine(FindPlayerDelayed());

        // ��������Ʈ ��û
        //requestCoroutine = StartCoroutine(SendGetRequest());
    }

    void Update() {
        if (player.activeInHierarchy && requestCoroutine == null) {
            // Player ���� ������Ʈ�� Ȱ��ȭ�ǰ� �ڷ�ƾ�� ���� ������ ������ �ڷ�ƾ�� ����
            //requestCoroutine = StartCoroutine(SendGetRequest());
            requestCoroutine = StartCoroutine(SendPostRequest());
        }
        else if (!player.activeInHierarchy && requestCoroutine != null) {
            // Player ���� ������Ʈ�� ��Ȱ��ȭ�ǰ� �ڷ�ƾ�� ���� ���̸� �ڷ�ƾ�� �ߴ�
            StopCoroutine(requestCoroutine);
            requestCoroutine = null;

            if (www != null) {
                www.Dispose(); // UnityWebRequest �ν��Ͻ��� ����
            }
        }
    }

    private void OnDisable() {
        Debug.Log("OnDisable ����");
        if(requestCoroutine != null) {
            StopCoroutine(requestCoroutine);
        }

        if(www != null) {
            www.Dispose();  // UnityWebRequest �ν��Ͻ� ����
        }
    }

    private void OnEnable() {
        Debug.Log("OnEnable ����");
        //requestCoroutine = StartCoroutine(SendGetRequest());
        requestCoroutine = StartCoroutine(SendPostRequest());
    }
    

    private void FindPlayer() {
        // Player�� ã�Ƽ� �Ҵ�
        player = GameObject.Find("Player1(Clone)");

        if (player == null) {
            Debug.LogError("Player1(Clone) not found!");
        }
        else {
            playerTransform = player.transform; // �÷��̾ ã������ transform �Ҵ�
        }
    }

    IEnumerator FindPlayerDelayed() {
        yield return new WaitForSeconds(1f); // 1�� ���

        FindPlayer(); // �÷��̾ ã�� �޼��� ȣ��
    }

    IEnumerator SendGetRequest() {

        while(true) {

            // Player ���� ������Ʈ ��Ȱ��ȭ �� �ڷ�ƾ �ߴ�
            if(!player.activeInHierarchy) {
                yield break;
            }

            Debug.Log("LoadData ����");
            www = UnityWebRequest.Get(url + "/test/get");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log("�� ������Ʈ ����");
                Debug.Log(www.error);
            }
            else {
                Debug.Log("�� ������Ʈ ����");
                string data = www.downloadHandler.text;
                Debug.Log(data);
            }

            if (www != null) {
                www.Dispose();  // UnityWebRequest �ν��Ͻ� ����
            }

            yield return new WaitForSeconds(0.06f);
        }
        
    }

    IEnumerator SendPostRequest() {

        WWWForm form;

        while(true) {

            // Player ���� ������Ʈ ��Ȱ��ȭ �� �ڷ�ƾ �ߴ�
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
                Debug.Log("�� ������Ʈ ����");
                Debug.Log(www.error);
            }
            else {
                Debug.Log("�� ������Ʈ ����");
                string data = www.downloadHandler.text;
                Debug.Log(data);
            }

            if (www != null) {
                www.Dispose();  // UnityWebRequest �ν��Ͻ� ����
            }

            yield return new WaitForSeconds(0.06f);
        }

    }
}
