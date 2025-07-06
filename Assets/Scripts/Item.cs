using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item",  menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea] 
    public string itemDesc; // �������� ����
    public ItemType itemType; // �������� ����
    public Sprite itemImage; // Sprite : Image�� �ٸ��� ����󿡼� �̹����� ���� ����� �� ����
    public GameObject itemPrefab; // �������� ������

    public string weaponType; // ���� ����

    // ������ 
    public enum ItemType
    {
        Equipment,
        Used,
        Ingredient,
        ETC
    }


    
}
