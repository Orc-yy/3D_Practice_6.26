using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{

    static public DragSlot instance;

    public Slot dragSlot;


    [SerializeField]
    private Image imageItem;

    private void Start()
    {
        instance = this;
    }


    public void DragSetImage(Image _imageItem)
    {
        imageItem.sprite = _imageItem.sprite;
        SetColor(0.5f);
    }

    public void SetColor(float _alpha)
    {
        Color color = imageItem.color;
        color.a = _alpha;
        imageItem.color = color;
    }
}
