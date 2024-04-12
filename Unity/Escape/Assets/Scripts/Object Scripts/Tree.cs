using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    // ������ ü��
    [SerializeField]
    private int hp;
    // ���� ���� �ð�
    [SerializeField]
    private float destroyTime;
    // ĸ�� �ݶ��̴�
    [SerializeField]
    private CapsuleCollider col;

    // �ʿ��� ���� ������Ʈ
    // �Ϲ� ����
    [SerializeField]
    private GameObject go_tree;
    // ���� ����
    [SerializeField]
    private GameObject go_debris;
    // ���� ����Ʈ
    [SerializeField]
    private GameObject go_effect_prefabs;
    // ȹ�� ������
    [SerializeField]
    private GameObject go_tree_item_prefab;
    [SerializeField]
    private GameObject go_leaf_item_prefab;

    // �ı� ����
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effect_sound;
    [SerializeField]
    private AudioClip effect_sound2;

    public void Felling()
    {
        audioSource.clip = effect_sound;
        audioSource.Play();
        Vector3 spawnPosition = col.bounds.center + col.transform.forward * (col.bounds.extents.z + 0.1f);
        var clone = Instantiate(go_effect_prefabs, spawnPosition, Quaternion.identity);
        Destroy(clone, 3f);

        if (WeaponManager.currentWeaponType == "AXE")
        {
            hp -= 2;
        }
        else
        {
            hp--;
        }

        if (hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        audioSource.clip = effect_sound2;
        audioSource.Play();
        Score.score += 1;

        col.enabled = false;
        // ������ ���� ������ ���� 1 ~ 3��
        int randomItemNum = Random.Range(1, 4);
        Vector3 spawnPosition = new Vector3(go_tree.transform.position.x, go_tree.transform.position.y + 1f, go_tree.transform.position.z);

        for (int i = 0; i < randomItemNum; i++)
        {
            Instantiate(go_tree_item_prefab, spawnPosition, Quaternion.identity);
        }

        Destroy(go_tree);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
