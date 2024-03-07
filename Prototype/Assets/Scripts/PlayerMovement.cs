using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5.0f;
    [SerializeField]
    float rotateSpeed = 10.0f;

    Vector3 moveDirection;

    private CharacterController characterController;
    private Animator animator;

    private void Awake()
    {
        // CharacterController를 통한 Move
        characterController = GetComponent<CharacterController>();
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // left, right, a, d 키 입력
        float h = Input.GetAxisRaw("Horizontal");
        // up, down, w, s 키 입력
        float v = Input.GetAxisRaw("Vertical");

        // 이동 방향을 키 입력에 따라 설정
        moveDirection = new Vector3(h, 0, v);

        // 바라보는 방향으로 회전 후 다시 정면을 바라보는 현상을 막기 위해 설정
        if (!(h == 0 && v == 0))
        {
            // 이동과 회전을 함께 처리
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
            // 회전하는 부분, Point 1.
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotateSpeed * Time.deltaTime);
        }

    }
}
