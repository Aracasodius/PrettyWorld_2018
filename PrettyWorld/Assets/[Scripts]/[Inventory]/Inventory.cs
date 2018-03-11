using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int space = 20;

    public delegate void onItemChanged();
    public onItemChanged onItemChangedCallback;

    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("ERROR 001, CALL DAVID");
        }
        instance = this;
    }

    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("INVENTORY FULL");
                return false;
            }

            items.Add(item);

            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
