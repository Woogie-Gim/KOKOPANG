using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // 나무의 체력
    [SerializeField]
    private int hp;
    // 파편 제거 시간
    [SerializeField]
    private float destroyTime;
    // 캡슐 콜라이더
    [SerializeField]
    private CapsuleCollider col;

    // 필요한 게임 오브젝트
    // 일반 나무
    [SerializeField]
    private GameObject go_tree;
    // 깨진 나무
    [SerializeField]
    private GameObject go_debris;
    // 벌목 이펙트
    [SerializeField]
    private GameObject go_effect_prefabs;
    // 획득 아이템
    [SerializeField]
    private GameObject go_tree_item_prefab;
    [SerializeField]
    private GameObject go_iron_item_prefab;

    // 파괴 사운드
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

        hp--;
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
        // 랜덤한 수의 아이템 생성 1 ~ 3개
        int treerandomItemNum = Random.Range(1, 4);
        int ironrandomItemNum = Random.Range(1, 4);

        Vector3 spawnPosition = new Vector3(go_tree.transform.position.x, go_tree.transform.position.y + 1f, go_tree.transform.position.z);

        for (int i = 0; i < treerandomItemNum; i++)
        {
            Instantiate(go_tree_item_prefab, spawnPosition, Quaternion.identity);
        }

        for (int i = 0; i < ironrandomItemNum; i++)
        {
            Instantiate(go_iron_item_prefab, spawnPosition, Quaternion.identity);
        }

        Destroy(go_tree);

        go_debris.SetActive(true);
        Destroy(go_debris, destroyTime);
    }
}
