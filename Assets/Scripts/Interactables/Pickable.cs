using UnityEngine;

public class Pickable : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    void PickUp()
    {
        var picked = Inventory.Instance.Add(item);

        if(picked)
            Destroy(gameObject);
    }
}
