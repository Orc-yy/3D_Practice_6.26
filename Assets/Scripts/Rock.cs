using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp; // Rock's health points

    [SerializeField]
    private float destroyTime; // ���� ���� �ð�

    [SerializeField]
    private SphereCollider col; // ��ü �ݶ��̴�

    [SerializeField]
    private GameObject go_rock; // �Ϲ� ����
    [SerializeField]
    private GameObject go_debris; // ���� ����
    [SerializeField]
    private GameObject go_effect_prefabs; // ���� ������


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
        Destroy(clone, destroyTime); // ����Ʈ ���� �ð�


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

        col.enabled = false; // �ݶ��̴� ��Ȱ��ȭ
        Destroy(go_rock);


        go_debris.SetActive(true); // ���� Ȱ��ȭ
        Destroy(go_debris, destroyTime);

    }

}
