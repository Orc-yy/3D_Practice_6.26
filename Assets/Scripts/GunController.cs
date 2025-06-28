using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
        if(currentFireRate > 0)
        {
            currentFireRate -= Time.deltaTime; // 60분의 1 = 1
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
        PlaySE(currentGun.fireSound); // 총알 발사 사운드 재생
        currentGun.muzzleFlash.Play(); // 총구 화염 효과 재생
        Debug.Log("총알이 발사됨");

    }
    
    private void PlaySE(AudioClip _clip)    
    {
        audioSource.clip = _clip;
        audioSource.Play();
    }
}
