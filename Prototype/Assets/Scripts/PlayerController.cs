using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 스피드 조정 변수
    [SerializeField]
    private float walkSpeed = 10.0f;
    [SerializeField]
    private float runSpeed = 20.0f;

    private float applySpeed;

    [SerializeField]
    private float jumpForce = 5.0f;

    // 상태 변수
    private bool isRun = false;
    private bool isGround = true;

    // 캡슐 콜라이더와 맵 meshCollider의 충돌 확인
    private CapsuleCollider capsuleCollider;

    // rigidbody를 통해 Player Control
    private Rigidbody myRigid;

    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        Move();
    }
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
    }
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }
    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }
    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }
    private void Move()
    {
        // A, D, Left, Right 키 입력 => 오른쪽 방향키 : 1, 왼쪽 방향키 : -1, 입력 X : 0 return
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        // W, S, Up, Down 키 입력 => 위쪽 방향키 : 1, 아래쪽 방향키 : -1, 입력 X : 0 reutrn
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

}
