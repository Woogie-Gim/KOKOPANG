using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    // 너클이나 맨손을 구분
    public string armsName;
    // 공격 범위
    public float range;
    // 공격력
    public int damage;
    // 작업 속도
    public float workSpeed;
    // 공격 딜레이
    public float attackDelay;
    // 공격 활성화 시정
    public float attackDelayA;
    // 공격 비활성화 시점
    public float attackDelayB;

    // 애니메이션
    public Animator anim;
}
