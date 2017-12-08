using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    Item item;
    InventorySlot slot;
    InventoryUI inventoryUI;
    WeaponSwitch weaponSwitch;
    public Image icon;
    public bool isSelected =false;
    public Image selected;
    public Item GetItem()
    {
        return item;
    }
    private void Awake()
    {
        inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryUI>();
        weaponSwitch = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponSwitch>();
        slot = GameObject.Find("WeaponSlot").GetComponent<InventorySlot>();
        slot.icon.enabled = false;
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            item = null;
            icon.enabled = false;
        }
    }
    private void Update()
    {
        selected.enabled = isSelected;
        if (slot.icon.enabled)
        {
            weaponSwitch.isArmed = true;
            weaponSwitch.SelectWeapon(slot.item.weaponID);
        }
        else
        {
            weaponSwitch.isArmed = false;
            weaponSwitch.DiselectWeapons();
        }

    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (slot.isSelected)
            {
                Item temp = slot.item;
                slot.AddItem(this.item);
                this.AddItem(temp);
                slot.isSelected = false;
                this.isSelected = false;
                inventoryUI.ChangeInventoryList();
                inventoryUI.AddWeapon(slot.item);


            }
            else if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                Item temp = this.item;
                this.AddItem(inventoryUI.slots[i].item);
                inventoryUI.slots[i].AddItem(temp);
                inventoryUI.slots[i].isSelected = false;
                isSelected = false;
                inventoryUI.ChangeInventoryList();
                inventoryUI.AddWeapon(slot.item);
                

            }
            
            


        }
        
        //selected.enabled = !selected.enabled;
    }

    public void OnWeaponSlotClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {

            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i] == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    Debug.Log("1");
                }
                else if (this == null)
                {
                    Item temp = this.item;
                    this.AddItem(inventoryUI.slots[i].item);
                    inventoryUI.slots[i].AddItem(temp);
                    Debug.Log("2");
                }
                else
                {
                    Item temp = this.item;
                    this.AddItem(inventoryUI.slots[i].item);
                    inventoryUI.slots[i].AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddWeapon(slot.item);
                    //playerEquip.ChangeWeapon(PlayerEquipWeapon.Weapon.tomahawk);
                    
                }

            }



        }
    }

    
}
