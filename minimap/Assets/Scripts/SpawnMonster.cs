using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject sushi1, sushi2, sushi3, sushi4, sushi5, sushi6;

    int SpawnObj;

    void Update()
    {
        SpwanPlay();
    }

    void SpwanPlay()
    {
        bool keydown = Input.GetKeyDown(KeyCode.Space);

        if (keydown)
        {
            SpawnObj = Random.Range(1, 7);

            switch (SpawnObj)
            {
                case 1:
                    Instantiate(sushi1, transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(sushi2, transform.position, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(sushi3, transform.position, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(sushi4, transform.position, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(sushi5, transform.position, Quaternion.identity);
                    break;
                case 6:
                    Instantiate(sushi6, transform.position, Quaternion.identity);
                    break;
            }
        }
    }
}


[DestroyZone.cs]

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}