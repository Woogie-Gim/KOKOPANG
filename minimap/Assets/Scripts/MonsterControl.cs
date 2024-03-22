using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    [SerializeField] Collider[] col;
    [SerializeField] float radius;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform target;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FindPlayer",0, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (col.Length > 0)
        {
            Quaternion dir = Quaternion.LookRotation(transform.position - target.position);
            Vector3 angle = Quaternion.RotateTowards(transform.rotation, dir, 200*Time.deltaTime).eulerAngles;
            transform.rotation = Quaternion.Euler(0, angle.y, 0);

            transform.Translate(Vector3.back * Time.deltaTime);


        }
    }

    void FindPlayer()
    {
        col = Physics.OverlapSphere(transform.position, radius, layer);
        Transform TCol = null;
        if (col.Length > 0)
        {
            TCol = col[0].transform;

        }
        target = TCol;

    }

}
