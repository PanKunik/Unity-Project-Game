using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;
    public int maxSize = 15;
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    private int size = 0;

    private void Awake()
    {
        
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    public List<Item> items = new List<Item>();

    private void Update()
    {

    }

    public bool AddToInventory(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (size  >= maxSize )
            {
                return false;
            }
            else
            {
                
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] == null)
                    {
                        items[i] = item;
                        size++;
                        break;
                    }
                    
                    
                }
                
            }
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();



        }
        return true;
    }
    public void SwapValues(Item item1, Item item2)
    {
        Item temp = item1;
        item1 = item2;
        item2 = temp;
    }



    public void DeleteFromInventory(Item item)
    {
        items.Remove(item);
    }


    public void ChangeItems(Item[] items)
    {
        for(int i = 0; i < this.items.Count; i++)
        {
            this.items[i] = items[i];
        }
    }
}
