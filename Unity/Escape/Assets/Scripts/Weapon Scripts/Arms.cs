using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    // ��Ŭ�̳� �Ǽ��� ����
    public string armsName;

    // ���� ����
    public bool isHand;
    public bool isAxe;
    public bool isPickAxe;

    // ���� ����
    public float range;
    // ���ݷ�
    public int damage;
    // �۾� �ӵ�
    public float workSpeed;
    // ���� ������
    public float attackDelay;
    // ���� Ȱ��ȭ ����
    public float attackDelayA;
    // ���� ��Ȱ��ȭ ����
    public float attackDelayB;

    // �ִϸ��̼�
    public Animator anim;
}
