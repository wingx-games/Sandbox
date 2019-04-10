using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> Items = new List<Item>();
    public int Space = 5;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    void Awake()
    {
        Instance = this;
    }

    public bool Add(Item item)
    {
        if(!item.IsDefaultItem ){
            if(Items.Count >= Space)
            {
                return false;
            }
            Items.Add(item);

            if(OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        Items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
