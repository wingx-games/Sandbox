using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform ItemsParents;
    public GameObject inventoryUI;
    Inventory Inventory;
    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = Inventory.Instance;
        Inventory.OnItemChangedCallback += UpdateUI;
        slots = ItemsParents.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length ; i++)
        {
            if(i < Inventory.Items.Count)
            {
                slots[i].AddItem(Inventory.Items[i]);
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
}
