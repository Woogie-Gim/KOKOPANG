using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    // 내 주변의 몬스터 찾기 알고리즘 구현
    [SerializeField] GameObject player;
    //Unity 게임 오브젝트를 그룹화 하고 동작을 수행하게 함
    [SerializeField] LayerMask layer;
    // 주변을 감지할 원의 범위 설정
    [SerializeField] float radius;
    // 원의 사이즈 안에 충돌하는게 있는지 확인
    [SerializeField] Collider[] col;

    [SerializeField] Transform target;



    [Header("적생성")]
    [SerializeField] GameObject Cenemy;
    [SerializeField] Transform[] Menemy;
    [SerializeField] float creatTime;


    [Header("카운트")]
    public int Count;
    [SerializeField] Text TextCount;
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("EnemyAround", 0, 0.2f);
        InvokeRepeating("EnemyCreate", 0, creatTime);
    }

    // 주변에 있는 적을 찾는것
    void EnemyAround()
    {
        col = Physics.OverlapSphere(player.transform.position, radius, layer);
        Transform minenemy = null;

        if (col.Length > 0)
        {
            float minDistance = Mathf.Infinity;

            foreach (Collider mCol in col)
            {
                float playerToDistance = Vector3.SqrMagnitude(player.transform.position - mCol.transform.position);
                if (playerToDistance < minDistance)
                {
                    minDistance = playerToDistance;
                    minenemy = mCol.transform;
                }
            }
        }
        target = minenemy;
    }





    void EnemyCreate()
    {
        int i = Random.Range(0,Menemy.Length);


        Instantiate(Cenemy, Menemy[i].position, Menemy[i].rotation);


    }

    private void Update()
    {
        // 주변에 타겟이 있는지 없는지 확인
        if (target == null)
        // 타겟이 없으면 빙글 빙글 돌아감
        {
            player.transform.Rotate(new Vector3(0, 60, 0) * Time.deltaTime);
        }
        else
        {
            // 적이 있으면 적이 있는 방향으로 몸을 돌림.
            Quaternion dir = Quaternion.LookRotation(target.position - player.transform.position);
           // 돌아가다가 그 적이 있는 방향으로 회전함
            Vector3 angle = Quaternion.RotateTowards(player.transform.rotation, dir, 200 * Time.deltaTime).eulerAngles;
            // 
            player.transform.rotation = Quaternion.Euler(0, angle.y, 0);
        
        }
    }




}
