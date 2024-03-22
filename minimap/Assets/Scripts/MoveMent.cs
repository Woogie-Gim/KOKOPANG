using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{

    // 코드 흐름은 선언 -> 초기화-> 호출

    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<T> : 자신의 T(타입) 컴포넌트를 가지고 온다.
        rigid = GetComponent<Rigidbody>();
        // AddForce(Vec) : Vec 의 방향과 크기로 힘을 줌.
        //rigid.AddForce(Vector3.up * 50, ForceMode.Impulse);




        //velocity => 현재 이동속도 ==> vector 값을 수신함
        // 이건 오른쪽 방향으로 속력이 정해짐
        //rigid.velocity = Vector3.right;


    }

    // Update is called once per frame
    void Update()
    {


        Vector3 vec = new Vector3(
            Input.GetAxisRaw("Horizontal") * Time.deltaTime,
            Input.GetAxisRaw("Jump") * Time.deltaTime,
            Input.GetAxisRaw("Vertical") * Time.deltaTime
            );
        transform.Translate(vec);



    }

    // 물체를 만들때 고려되어야 할 필수 요소 
    //Mesh,Material, Collider, RigidBody 
    //Friction => 마찰계수


    // 물리조건을 안정되게 받기 위해서는 FixedUpdate 로 적어주는게 좋음
    private void FixedUpdate()
    {
       
        //if (Input.GetButtonDown("Jump"))
        //{
        //    rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        //}
        //Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),
        //    0, Input.GetAxisRaw("Vertical"));

        //rigid.AddForce(vec * Time.deltaTime, ForceMode.Impulse);

        ////#3.회전력 
        ////rigid.AddTorque(Vector3.down);

    }
}
