using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",  menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType; // 아이템의 유형
    public Sprite itemImage; // Sprite : Image와 다르게 월드상에서 이미지를 직접 출력할 수 있음
    public GameObject itemPrefab; // 아이템의 프리팹

    public string weaponType; // 무기 유형

    // 열거형 
    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }


    
}
