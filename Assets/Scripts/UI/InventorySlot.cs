using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image Icon;
    Item item;
    [SerializeField]
    Button RemoveButton;

    public void AddItem(Item newItem)
    {
        item = newItem;
        Icon.sprite = item.Icon;
        Icon.enabled = true;
        RemoveButton.interactable = true;
    }


    public void Clear()
    {
        item = null;
        Icon.sprite = null;
        Icon.enabled = false;
        RemoveButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}
