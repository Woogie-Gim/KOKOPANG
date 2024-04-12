using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ore : MonoBehaviour
{
    // 바위의 체력
    [SerializeField]
    private int hp;
    // 파편 제거 시간
    [SerializeField]
    private float destroyTime;
    // 박스 콜라이더
    [SerializeField]
    private BoxCollider col;

    // 필요한 게임 오브젝트
    [SerializeField]
    private GameObject go_ore;
    // 깨진 바위
    [SerializeField]
    private GameObject go_debris;
    // 채굴 이펙트
    [SerializeField]
    private GameObject go_effect_prefabs;
    // 획득 아이템
    [SerializeField]
    private GameObject go_ore_item_prefab;

    // 파괴 사운드
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

        // 랜덤한 수의 아이템 생성 1 ~ 3개
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
