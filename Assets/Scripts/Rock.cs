using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // Rock's health points

    [SerializeField]
    private float destroyTime; // 파편 제거 시간

    [SerializeField]
    private SphereCollider col; // 구체 콜라이더

    [SerializeField]
    private GameObject go_rock; // 일반 바위
    [SerializeField]
    private GameObject go_debris; // 꺠진 바위
    [SerializeField]
    private GameObject go_effect_prefabs; // 파편 프리팹


    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip effect_soud;
    [SerializeField]
    private AudioClip effect_soud2;


    public void Mining()
    {
        audioSource.clip = effect_soud;
        audioSource.Play();

        var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime); // 이펙트 제거 시간


        hp--;
        if(hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        audioSource.clip = effect_soud2;
        audioSource.Play();

        col.enabled = false; // 콜라이더 비활성화
        Destroy(go_rock);


        go_debris.SetActive(true); // 파편 활성화
        Destroy(go_debris, destroyTime);

    }

}
