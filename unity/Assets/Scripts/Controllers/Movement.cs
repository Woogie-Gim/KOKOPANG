using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour {
    private CharacterController controller;
    private new Transform transform;
    private new Camera camera;

    private Plane plane;
    //private PhotonView pv;

    public float moveSpeed = 10.0f;
    public float gravity = -9.81f; // 
    private Vector3 velocity; //

    private Vector3 receivePos;
    private Quaternion receiveRot;
    public float damping = 10.0f;

    private Animator animator;
    private Rigidbody2D rb;
    public TMP_Text nicknameText;

    void Start() {
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //pv = GetComponent<PhotonView>();

        plane = new Plane(transform.up, transform.position);

        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        //if (pv.IsMine) {
            Move();
            PlayerRotate();
            //nicknameText.text = PhotonNetwork.NickName;
            nicknameText.color = Color.white;
        //}
        //else {
        //    // 
        //    transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * damping);

        //    //
        //    transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);

        //    nicknameText.text = photonView.Owner.NickName;
        //    nicknameText.color = new Color(Random.value, Random.value, Random.value);
        //}
    }

    public void Emotion() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            animator.SetBool("Emotion", true);
        }
        else {
            animator.SetBool("Emotion", false);
        }
    }

    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");

    public void Move() {
        Vector3 camForward = camera.transform.forward;
        camForward.y = 0.0f;

        Vector3 camRight = camera.transform.right;
        camRight.y = 0.0f;

        Vector3 heading = (camForward * v) + (camRight * h);
        heading.Set(heading.x, 0.0f, heading.z);

        if (controller.isGrounded) {
            velocity.y = 0f;
        }
        velocity.y += gravity * Time.deltaTime;

        Vector3 moveDirection = heading * moveSpeed;
        moveDirection.y = velocity.y; // 

        controller.Move(moveDirection * Time.deltaTime);

        float forwardDot = Vector3.Dot(heading, transform.forward);
        float strafeDot = Vector3.Dot(heading, transform.right);

        animator.SetFloat("Forward", forwardDot);
        animator.SetFloat("Strafe", strafeDot);
    }

    public void PlayerRotate() {
        Ray CameraRay = camera.ScreenPointToRay(Input.mousePosition);

        float rayDistance = 0.0f;
        plane.Raycast(CameraRay, out rayDistance);
        Vector3 hit = CameraRay.GetPoint(rayDistance);

        Vector3 look = hit - transform.position;
        look.y = 0;

        if (h == 0 && v == 0) {
            transform.localRotation = Quaternion.LookRotation(look);
        }
        else {
            Vector3 camLook = camera.transform.forward;
            camLook.y = 0;
            transform.localRotation = Quaternion.LookRotation(camLook);
        }
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    //    if (stream.IsWriting) {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    else {
    //        receivePos = (Vector3)stream.ReceiveNext();
    //        receiveRot = (Quaternion)stream.ReceiveNext();
    //    }
    //}
}
