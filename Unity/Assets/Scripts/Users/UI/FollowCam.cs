using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;
    private GameObject player;

    public Transform target; // Player

    private float rotSensitive = 3f; // 카메라 회전 감도
    private float dis = 5f; // 카메라와 플레이어 사이의 거리
    private float RotationMin = -10f; // 카메라 회전 각도 최소
    private float RotationMax = 80f; // 카메라 회전 각도 최대
    private float smoothTime = 0.12f; // 카메라가 회전하는데 걸리는 시간
    private float desiredHeight = 1.5f; // 원하는 카메라 높이
    private Vector3 targetRotation;
    private Vector3 currentVel;

    private void Start()
    {
        // Player를 찾아서 할당
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
        yield return new WaitForSeconds(1f); // 1초 대기

        //FindPlayer(); // 플레이어를 찾는 메서드 호출
    }

    //private void FindPlayer()
    //{
    //    // Player를 찾아서 할당
    //    player = GameObject.Find("Player1(Clone)");

    //    if (player == null)
    //    {
    //        Debug.LogError("Player1(Clone) not found!");
    //    }
    //    else
    //    {
    //        target = player.transform; // 플레이어를 찾았으면 target을 설정
    //    }
    //}
}
