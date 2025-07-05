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

    // 필요한 게임 오브젝트
    [SerializeField]
    private GameObject go_rock; // 일반 바위
    [SerializeField]
    private GameObject go_debris; // 꺠진 바위
    [SerializeField]
    private GameObject go_effect_prefabs; // 파편 프리팹
    [SerializeField]
    private GameObject go_rock_item_prefab; // 돌맹이 아이템

    // 돌맹이 아이템 등장 개수
    [SerializeField]
    private int count;

    // 필요한 사운드 이름
    [SerializeField]
    private string strike_Sound;
    [SerializeField]
    private string destroy_Sound;


    public void Mining()
    {
        SoundManager.instance.PlaySE(strike_Sound);

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
        SoundManager.instance.PlaySE(destroy_Sound);

        col.enabled = false; // 콜라이더 비활성화
        for (int i = 0; i <= count; i++)
        {
            Instantiate(go_rock_item_prefab, go_rock.transform.position, Quaternion.identity);
        }
        Destroy(go_rock);
        go_debris.SetActive(true); // 파편 활성화
        Destroy(go_debris, destroyTime);

    }

}
