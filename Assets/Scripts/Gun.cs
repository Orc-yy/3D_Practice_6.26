using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;
    public float range;      // 총기의 사거리
    public float accuracy; // 총기의 정확도
    public float fireRate; // 총기의 발사 속도
    public float reloadTime; // 재장전 속도

    public int damage;

    public int reloadBulletCount; // 총알 재장전 개수
    public int currentBulletCount; // 현재 탄알집에 남아있는 총알 개수
    public int maxBulletCount; // 총알의 최대 소유 가능 개수
    public int carryBulletCount; // 현재 소유하고 있는 총알 개수

    public float retroActionForce; // 반동 세기
    public float retroActionFineSightForce; // 정조준시의 반동 세기

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlash; // 총구 화염

    public AudioClip fireSound;


    
}
