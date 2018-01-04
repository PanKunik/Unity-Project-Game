using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    //public static InventoryUI instance;
    public Transform itemsParent;
    InventoryPlayer inventory;
    public InventorySlot[] slots { get; set; }

    private void Start()
    {
        inventory = InventoryPlayer.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    private void Update()
    {
        

    }
   /* void Function()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSelected)
                sum++;
        }
    }*/
    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
                slots[i].ClearSlot();
        }
        //Debug.Log("UpdateUI");
    }

    public void ChangeInventoryList()
    {
        Item[] arr = new Item[slots.Length];
        for(int i = 0; i < slots.Length; i++)
        {
            arr[i] = slots[i].GetItem(); 
        }
        inventory.ChangeItems(arr);
    }
    
    public void AddWeapon(Item weapon)
    {
        inventory.weapon = weapon;

    }

    public void AddArmor(Item armor)
    {
        inventory.armor = armor;
    }
    
}
