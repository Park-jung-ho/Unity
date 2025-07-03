using UnityEngine;

public class ItemCoin : MonoBehaviour, IItemObject
{
    public ItemManager inventory { get; set; }
    public GameObject Obj { get; set; }
    public string ItemName { get; set; }
    public Sprite Icon { get; set; }

    void Start()
    {
        Obj = gameObject;
        ItemName = name;
        Icon = GetComponent<SpriteRenderer>().sprite;
    }

    public void Get()
    {
        gameObject.SetActive(false);
        inventory.GetItem(this);
    }

    public void Use()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Get();
        }
    }
}
