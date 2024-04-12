using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ore : MonoBehaviour
{
    // ������ ü��
    [SerializeField]
    private int hp;
    // ���� ���� �ð�
    [SerializeField]
    private float destroyTime;
    // �ڽ� �ݶ��̴�
    [SerializeField]
    private BoxCollider col;

    // �ʿ��� ���� ������Ʈ
    [SerializeField]
    private GameObject go_ore;
    // ���� ����
    [SerializeField]
    private GameObject go_debris;
    // ä�� ����Ʈ
    [SerializeField]
    private GameObject go_effect_prefabs;
    // ȹ�� ������
    [SerializeField]
    private GameObject go_ore_item_prefab;

    // �ı� ����
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effect_sound;
    [SerializeField]
    private AudioClip effect_sound2;

    public void Mining()
    {
        audioSource.clip = effect_sound;
        audioSource.Play();
        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        Destroy(clone, 3f);

        if (WeaponManager.currentWeaponType == "PICKAXE")
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
        col.enabled = false;
        Score.score += 2;

        // ������ ���� ������ ���� 1 ~ 3��
        int randomItemNum = Random.Range(1, 4);
        Vector3 spawnPosition = new Vector3(go_ore.transform.position.x, go_ore.transform.position.y, go_ore.transform.position.z);

        for (int i = 0; i < randomItemNum; i++)
        {
            Instantiate(go_ore_item_prefab, spawnPosition, Quaternion.identity);
        }

        Destroy(go_ore);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
