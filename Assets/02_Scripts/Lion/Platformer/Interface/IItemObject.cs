using UnityEngine;

public interface IItemObject
{
    ItemManager inventory { get; set; }
    GameObject Obj { get; set; }
    string ItemName { get; set; }
    Sprite Icon { get; set; }

    void Get();
    void Use();
}
