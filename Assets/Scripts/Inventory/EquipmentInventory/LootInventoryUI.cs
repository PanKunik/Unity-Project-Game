using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LootInventoryUI : MonoBehaviour {
    LootInventory inventory;

    public Transform itemsParent;
    public InventorySlot[] slots { get; set; }



    // Use this for initialization
    void Start () {
        inventory = LootInventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClicked()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSelected && slots[i].icon.enabled)
            {
                InventoryPlayer.instance.AddToInventory(slots[i].GetItem());
                slots[i].ClearSlot();
                ChangeInventoryList();
                slots[i].isSelected = false;
            }
            else
                continue;
        }
    }

    void UpdateUI()
    {
        Debug.Log(LootInventory.instance);
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
                slots[i].ClearSlot();
        }

    }

    public void ChangeInventoryList()
    {
        inventory = LootInventory.instance;
        Item[] arr = new Item[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            arr[i] = slots[i].GetItem();
        }
        inventory.ChangeItems(arr);
    }

}
