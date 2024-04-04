using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    // 동물의 이름
    [SerializeField]
    private string animalName;
    // 동물의 체력
    [SerializeField]
    private int hp;
    // 걷기 스피드
    [SerializeField]
    private float walkSpeed;
    // 뛰는 스피드
    [SerializeField]
    private float runSpeed;
    private float applySpeed;

    // 방향
    private Vector3 direction; 

    // 상태 변수
    private bool isWalking;
    private bool isAction;
    private bool isRunning;
    private bool isDead;

    // 걷기 시간
    [SerializeField]
    private float walkTime;
    // 대기 시간
    [SerializeField]
    private float waitTime;
    // 뛰는 시간
    [SerializeField]
    private float runTime;
    private float currentTIme;

    // 필요한 컴포넌트
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private BoxCollider boxcol;
    private AudioSource theAudio;
    [SerializeField]
    private AudioClip[] sound_pig_Normal;
    [SerializeField]
    private AudioClip sound_pig_Hurt;
    [SerializeField]
    private AudioClip sound_pig_Dead;
    [SerializeField]
    private GameObject go_Pork_Prefab;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        currentTIme = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
        {
            rigid.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
        }
    }

    private void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTIme -= Time.deltaTime;
            if (currentTIme <= 0)
            {
                // 다음 랜덤 행동 개시
                ResetAnim();
            }
        }
    }

    private void RandomAction()
    {
        isAction = true;

        int _random = Random.Range(0, 4);

        if (_random == 0)
        {
            Wait();
        }
        else if (_random == 1)
        {
            Eat();
        }
        else if (_random == 2)
        {
            Peek();
        }
        else if (_random == 3)
        {
            TryWalk();
        }
    }

    private void ResetAnim()
    {
        RandomSound();
        isAction = false;
        isWalking = false;
        isRunning = false;
        applySpeed = walkSpeed;
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }
    private void Wait()
    {
        currentTIme = waitTime;
    }
    private void Eat()
    {
        currentTIme = waitTime;
        anim.SetTrigger("Eat");
    }
    private void Peek()
    {
        currentTIme = waitTime;
        anim.SetTrigger("Peek");
    }
    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTIme = walkTime;
        applySpeed = walkSpeed;
    }

    private void Run(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;

        currentTIme = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;
        anim.SetBool("Running", isRunning);
    }

    public void Damage(Vector3 _targetPos)
    {
        if (!isDead)
        {
            if (WeaponManager.currentWeaponType == "AXE" || WeaponManager.currentWeaponType == "PICKAXE")
            {
                hp -= 2;
            }
            else
            {
                hp--;
            }

            if (hp <= 0)
            {
                Dead();
                return;
            }

            PlaySE(sound_pig_Hurt);
            anim.SetTrigger("Hit");
            Run(_targetPos);
        }

    }

    private void Dead()
    {
        PlaySE(sound_pig_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Death");

        // 랜덤한 수의 아이템 생성 1 ~ 3개
        int randomItemNum = Random.Range(1, 4);
        Vector3 spawnPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z);

        for (int i = 0; i < randomItemNum; i++)
        {
            Instantiate(go_Pork_Prefab, spawnPosition, Quaternion.identity);

        }

        StartCoroutine(DestroyPig());
    }
    private void RandomSound()
    {
        int _random = Random.Range(0, 3);
        PlaySE(sound_pig_Normal[_random]);
    }

    private void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }

    IEnumerator DestroyPig()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
