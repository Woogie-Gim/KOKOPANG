using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;
    private GameObject player;

    public Transform target; // Player

    private float rotSensitive = 3f; // ī�޶� ȸ�� ����
    private float dis = 5f; // ī�޶�� �÷��̾� ������ �Ÿ�
    private float RotationMin = -10f; // ī�޶� ȸ�� ���� �ּ�
    private float RotationMax = 80f; // ī�޶� ȸ�� ���� �ִ�
    private float smoothTime = 0.12f; // ī�޶� ȸ���ϴµ� �ɸ��� �ð�
    private float desiredHeight = 1.5f; // ���ϴ� ī�޶� ����
    private Vector3 targetRotation;
    private Vector3 currentVel;

    private void Start()
    {
        // Player�� ã�Ƽ� �Ҵ�
        StartCoroutine(FindPlayerDelayed());
    }

    void LateUpdate()
    {
        Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;
        Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;

        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        this.transform.eulerAngles = targetRotation;

        transform.position = target.position + Vector3.up * desiredHeight - transform.forward * dis;
    }

    IEnumerator FindPlayerDelayed()
    {
        yield return new WaitForSeconds(1f); // 1�� ���

        //FindPlayer(); // �÷��̾ ã�� �޼��� ȣ��
    }

    //private void FindPlayer()
    //{
    //    // Player�� ã�Ƽ� �Ҵ�
    //    player = GameObject.Find("Player1(Clone)");

    //    if (player == null)
    //    {
    //        Debug.LogError("Player1(Clone) not found!");
    //    }
    //    else
    //    {
    //        target = player.transform; // �÷��̾ ã������ target�� ����
    //    }
    //}
}
