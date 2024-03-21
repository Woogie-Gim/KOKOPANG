using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Gun currentGun;

    private float currentFireRate;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GunFireRateCalc();
        TryFire();
    }

    private void GunFireRateCalc()
    {
        if (currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime; // 1초의 역수 (60분위 1)
        }
    }

    private void TryFire()
    {
        if (Input.GetButton("Fire1") && currentFireRate <= 0)
        {
            Fire();
        }
    }

    // 발사 전
    private void Fire()
    {
        currentFireRate = currentGun.fireRate;
        Shoot();
    }

    // 발사 후
    private void Shoot()
    {
        PlaySoundEffect(currentGun.fire_Sound);
        currentGun.muzzleFlash.Play();
        Debug.Log("총알 발사함");
    }

    private void PlaySoundEffect(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
