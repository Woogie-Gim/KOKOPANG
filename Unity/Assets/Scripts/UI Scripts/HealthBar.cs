using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    // 플레이어의 HP
    private int hp;
    public static bool isDead = false;


    private void Awake()
    {
        // 플레이어의 HP 값을 초기 세팅
        hp = 10;
        SetMaxHealth(hp);
    }

    public void SetMaxHealth(int health)
    {
        hpBar.maxValue = health;
        hpBar.value = health;
    }

    public void Damage()
    {
        if (!isDead)
        {
            Debug.Log("Attack");
            Debug.Log(hp);
            if (WeaponManager.currentWeaponType == "AXE" || WeaponManager.currentWeaponType == "PICKAXE")
            {
                hp -= 2;
            }
            else
            {
                hp--;
            }

            // 플레이어가 데미지를 받으면서 슬라이더 값도 변경
            hpBar.value = hp;

            if (hp <= 0)
            {
                Dead();
                return;
            }
        }

    }

    private void Dead()
    {
        isDead = true;
    }

}
