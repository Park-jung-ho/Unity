using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject[] items;
    [SerializeField] private Transform slotGroup;
    public Slot[] slots;

    private void Start()
    {
        slots = slotGroup.GetComponentsInChildren<Slot>(true);
    }

    public void InventoryActive()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void DropItem(Vector3 dropPos)
    {
        var randomIndex = Random.Range(0, items.Length);

        GameObject item = Instantiate(items[randomIndex], dropPos, Quaternion.identity);
        item.GetComponent<IItemObject>().inventory = this;

        Rigidbody2D itemRb = item.GetComponent<Rigidbody2D>();

        itemRb.AddForceX(Random.Range(-2f, 2f), ForceMode2D.Impulse);
        itemRb.AddForceY(5f, ForceMode2D.Impulse);

        float ranPower = Random.Range(-1.5f, 1.5f);
        itemRb.AddTorque(ranPower, ForceMode2D.Impulse);
    }

    public void GetItem(IItemObject itemObject)
    {
        // in to inventory
        foreach (var slot in slots)
        {
            if (!slot.isEmpty) continue;
            slot.AddItem(itemObject);
            return;
        }
    }
}
