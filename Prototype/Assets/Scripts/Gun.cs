using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // 총의 이름
    public string gunName;
    // 사정 거리
    public float range;
    // 정확도
    public float accuracy;
    // 연사 속도
    public float fireRate;
    // 재장전 속도
    public float reloadTime;

    // 총의 데미지
    public int damage;

    // 총알 재장전 개수
    public int reloadBulletCount;
    // 현재 탄알집에 남아 있는 총알의 개수
    public int currentBulletCount;
    // 최대 소유 가능 개수
    public int maxBulletCount;
    // 현재 소유 하고 있는 총알의 개수
    public int carryBulletCount;

    // 반동 세기
    public float retroActionForce;
    // 정조준 시의 반동 세기
    public float retroActionFineSightForce;

    public Vector3 fineSightOriginPos;

    public Animator anim;

    // 총구 섬광을 위한 파티클 시스템
    public ParticleSystem muzzleFlash;

    public AudioClip fire_Sound;
}
