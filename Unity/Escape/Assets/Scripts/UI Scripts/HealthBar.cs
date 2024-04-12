using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;

    // �÷��̾��� HP
    private int hp;
    public static bool isDead = false;


    private void Awake()
    {
        // �÷��̾��� HP ���� �ʱ� ����
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

            // �÷��̾ �������� �����鼭 �����̴� ���� ����
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
