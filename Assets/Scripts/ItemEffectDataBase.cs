using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class ItemEffect
{
    public string itemName; // 아이템의 이름  (키값)
    [Tooltip("HP,SP,DP, HUNGRY, THIRSTY, SATISFY만 가능합니다")]
    public string[] part; // 부위
    public int[] num; // 수치 
}

public class ItemEffectDataBase : MonoBehaviour
{
    [SerializeField]
    private ItemEffect[] itemEffects;


    // 필요한 컴포넌트 
    [SerializeField]
    private StatusController thePlayerStatus;
    [SerializeField]
    private WeaponManager theWeaponManager;

    private const string HP = "HP", SP = "SP", DP = "DP", HUNGRY = "HUNGRY", THIRSTY = "THIRSTY", SATISFY = "SATISFY";

    public void UseItem(Item _item)
    {

        if (_item.itemType == Item.ItemType.Equipment)
        {
            // 장착
            StartCoroutine(theWeaponManager.ChangeWeaponCoroutine(_item.weaponType, _item.itemName));
        }
        else if (_item.itemType == Item.ItemType.Used)
        {
            for(int i = 0;  i < itemEffects.Length; i++)
            {
                if (itemEffects[i].itemName == _item.itemName)
                {
                    for(int j = 0; j < itemEffects[i].part.Length; j++)
                    {
                        switch(itemEffects[i].part[j])
                        {
                            case HP:
                                thePlayerStatus.IncreaseHP(itemEffects[i].num[j]);
                                break;
                            case SP:
                                thePlayerStatus.IncreaseSP(itemEffects[i].num[j]);
                                break;
                            case DP:
                                thePlayerStatus.IncreaseDP(itemEffects[i].num[j]);
                                break;
                            case HUNGRY:
                                thePlayerStatus.IncreaseHungry(itemEffects[i].num[j]);
                                break;
                            case THIRSTY:
                                thePlayerStatus.IncreaseThirsty(itemEffects[i].num[j]);
                                break;
                            case SATISFY:
                                break;
                            default:
                                Debug.Log("잘못된 Status 부위");
                                break;

                        }
                        Debug.Log(_item.itemName + " 을 사용했습니다.");
                    }
                    return;
                }
            }
        }
        Debug.Log("itemEffectDataBase에 일치하는 itemName이 없습니다.");
    }
}
