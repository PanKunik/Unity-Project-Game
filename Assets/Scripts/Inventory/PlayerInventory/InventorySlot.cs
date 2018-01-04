using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour {
    Item item { get; set; }
    Weapon weapon { get; set; }
    InventorySlot weaponSlot;
    InventorySlot armorSlot;
    InventoryUI inventoryUI;
    WeaponSwitch weaponSwitch;
    PlayerStats playerStats;
    PlayerCombat playerCombat;

    public int slotTypeID;
    public Image icon;
    public bool isSelected = false;
    public Image selected;

    public Item GetItem()
    {
        
        return item;
    }

    private void Awake()
    {
        inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryUI>();
        weaponSwitch = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponSwitch>();
        weaponSlot = GameObject.Find("WeaponSlot").GetComponent<InventorySlot>();
        weaponSlot.icon.enabled = false;
        armorSlot = GameObject.Find("ArmorSlot").GetComponent<InventorySlot>();
        armorSlot.icon.enabled = false;
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        this.icon.enabled = false;

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


        if (weaponSlot.icon.enabled)
        {
            weapon = (Weapon)weaponSlot.item;
            weaponSwitch.isArmed = true;
            weaponSwitch.SelectWeapon(weapon.weaponID);
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
            if (armorSlot.isSelected)                       //changing armor item in inventory, check if selected armorslot
            {
                if (this.item != null)
                {
                    if (this.item.itemID == 1)
                    {
                        Item tempItem = armorSlot.item;
                        armorSlot.AddItem(this.item);
                        this.AddItem(tempItem);
                        armorSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddArmor(armorSlot.item);
                        //ChangePlayerProperties();
                    }
                    else
                    {
                        this.isSelected = false;
                        armorSlot.isSelected = false;
                    }

                }
                else
                {
                    Item temp = armorSlot.item;
                    armorSlot.AddItem(this.item);
                    this.AddItem(temp);
                    armorSlot.isSelected = false;
                    this.isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddArmor(armorSlot.item);
                    //ChangePlayerProperties();
                }
            }

            if (weaponSlot.isSelected) 
            {

                if (this.item != null)
                {
                    if (this.item.itemID == 0)
                    {

                        Item temp = weaponSlot.item;
                        weaponSlot.AddItem(this.item);
                        this.AddItem(temp);
                        weaponSlot.isSelected = false;
                        this.isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddWeapon(weaponSlot.item);
                        //ChangePlayerProperties();

                    }
                    else
                    {
                        weaponSlot.isSelected = false;
                        this.isSelected = false;

                    }
                }
                else
                {
                    Item temp = weaponSlot.item;
                    weaponSlot.AddItem(this.item);
                    this.AddItem(temp);
                    weaponSlot.isSelected = false;
                    this.isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddWeapon(weaponSlot.item);
                    //ChangePlayerProperties();
                }
                Debug.Log("1");
            }
            else if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                Item temp = this.item;
                this.AddItem(inventoryUI.slots[i].item);
                inventoryUI.slots[i].AddItem(temp);
                inventoryUI.slots[i].isSelected = false;
                isSelected = false;
                inventoryUI.ChangeInventoryList();
                inventoryUI.AddWeapon(weaponSlot.item);
                //ChangePlayerProperties();
                Debug.Log("2");
            }

        }
        
    }

    public void OnWeaponSlotClicked()
    {
        
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i].item == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    inventoryUI.AddWeapon(weaponSlot.item);
                    Debug.Log("3");
                }//if there is no item in slot u selected before
                else if (this.item == null)
                {
                    if (inventoryUI.slots[i].item.itemID == 0) 
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddWeapon(weaponSlot.item);
                        
                        

                    } // if selected items can be fitted
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;

                    } //if selected items in slots cannot be fitted
                    Debug.Log("4");
                }//if there is no item in slot u have just selected
                else
                {
                    if (inventoryUI.slots[i].item.itemID == 0)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddWeapon(weaponSlot.item);
                    }
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                    }
                    Debug.Log("5");
                }

            }

        }
        weapon = (Weapon)weaponSlot.item;
        if (weapon)
        {
            WeaponChangePlayerStats(weapon.damage, weapon.speedAttack);
        }
    }//all conditions to change weapon items with weapon slot

    public void OnArmorSlotClicked()
    {
        isSelected = !isSelected;
        for (int i = 0; i < inventoryUI.slots.Length; i++)
        {
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i].item == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.AddArmor(armorSlot.item);
                    Debug.Log("6");
                }//if there is no item u selected before 
                else if (this.item == null)
                {
                    if (inventoryUI.slots[i].item.itemID == 1)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddArmor(armorSlot.item);
                        isSelected = false;

                    } // if item can be fitted to the slot
                    else 
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;

                    } // if item is not fitted that slot
                    Debug.Log("7");
                }//if there is no item on selected slot
                else 
                {
                    if (inventoryUI.slots[i].item.itemID == 1)
                    {
                        Item temp = this.item;
                        this.AddItem(inventoryUI.slots[i].item);
                        inventoryUI.slots[i].AddItem(temp);
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                        inventoryUI.ChangeInventoryList();
                        inventoryUI.AddArmor(armorSlot.item);
                    }//check if slot and item can be fitted (true)
                    else
                    {
                        inventoryUI.slots[i].isSelected = false;
                        isSelected = false;
                    }//(false)
                    Debug.Log("8");
                }////if there is item on selected slot

            }



        }
    } //all conditions to change armor items with armor slot

    void WeaponChangePlayerStats(int damage, float speedAttack)
    {
        playerStats.damage.SetValue(damage);
        playerCombat.attackSpeed = speedAttack;
        Debug.Log("SpeedAttacK: " + speedAttack + "\nDamage: " + damage);
    }
    
}
