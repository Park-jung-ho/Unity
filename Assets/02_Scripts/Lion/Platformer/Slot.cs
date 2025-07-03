using System;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private IItemObject item;
    public Image itemImage;
    public Button slotButton;
    
    public bool isEmpty = true;
    void Awake()
    {
        slotButton.onClick.AddListener(UseItem);
    }

    void UpdateSlot()
    {
        slotButton.interactable = !isEmpty;
        itemImage.gameObject.SetActive(!isEmpty);
    }
    void OnEnable()
    {
        UpdateSlot();
    }

    public void AddItem(IItemObject newItem)
    {
        isEmpty = false;
        item = newItem;
        itemImage.sprite = newItem.Icon;
        itemImage.SetNativeSize();
        UpdateSlot();
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        isEmpty = true;
        UpdateSlot();
    }
}
