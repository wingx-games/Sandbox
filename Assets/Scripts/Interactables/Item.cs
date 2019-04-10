using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string Name = "New item";
    public Color Color = Color.blue;
    public bool IsDefaultItem = false;
    public Sprite Icon;

    public virtual void Use()
    {
        FindObjectOfType<PlayerController>().Grow();
        Inventory.Instance.Remove(this);
    }
}
