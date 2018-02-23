using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    public Transform itemsParent;
    InventoryPlayer inventory;
    public InventorySlot[] slots { get; set; }
    Text currency;
    Text availableSlots;


    private void Start()
    {
        inventory = InventoryPlayer.playerInventory;
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        currency = GameObject.Find("Сurrency").GetComponent<Text>();
        availableSlots = GameObject.Find("AvailableSlot").GetComponent<Text>();
        instance = this;
        UpdateUI();
    }

    private void Update()
    {
        UpdateUIText();
    }

    void UpdateUIText()
    {
        currency.text = "Currency: " + inventory.rippleAmount.ToString();
        int count = 0;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetItem())
                count++;

        }
        availableSlots.text = count + "/" + inventory.maxSize.ToString();
    }

    void UpdateUI()
    {
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
        Item[] arr = new Item[slots.Length];
        for (int i = 0; i < slots.Length; i++)
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

    public void AddHelmet(Item helmet)
    {
        inventory.helmet = helmet;
    }

    public void AddGloves(Item gloves)
    {
        inventory.gloves = gloves;
    }

    public void AddBoots(Item boots)
    {
        inventory.boots = boots;
    }

    public void OnDropBtnClicked()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].isSelected && slots[i].icon.enabled)
            {
                if (slots[i].GetItem() is LootItem)
                {
                    LootItem lootItem = (LootItem)slots[i].GetItem();
                    switch (lootItem.lootID)
                    {
                        case 0:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.stoneAmount;
                            inventory.stoneAmount = 0;
                            break;
                        case 1:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.woodAmount;
                            inventory.woodAmount = 0;
                            break;
                        case 2:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.steelAmount;
                            inventory.steelAmount = 0;
                            break;
                        case 3:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.coalAmount;
                            inventory.coalAmount = 0;
                            break;
                        case 4:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.clayAmount;
                            inventory.clayAmount = 0;
                            break;
                        case 5:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.leatherAmount;
                            inventory.leatherAmount = 0;
                            break;
                        case 6:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.boneAmount;
                            inventory.boneAmount = 0;
                            break;
                        case 7:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.glassAmount;
                            inventory.glassAmount = 0;
                            break;
                        case 8:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.diamondAmount;
                            inventory.diamondAmount = 0;
                            break;
                        case 9:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.bigPotsAmount;
                            inventory.bigPotsAmount = 0;
                            break;
                        case 10:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.middlePotsAmount;
                            inventory.middlePotsAmount = 0;
                            break;
                        case 11:
                            slots[i].GetItem().item.GetComponent<ItemPickup>().amountItemPickup = inventory.smallPotsAmount;
                            inventory.smallPotsAmount = 0;
                            break;
                    }
                    DropItem(slots[i].GetItem());
                    slots[i].ClearSlot();
                    ChangeInventoryList();
                    slots[i].isSelected = false;
                }
                else
                {
                    DropItem(slots[i].GetItem());
                    slots[i].ClearSlot();
                    ChangeInventoryList();
                    slots[i].isSelected = false;
                }


            }
        }
    }

    public void OnExitBtnClicked()
    {
        GameObject.FindGameObjectWithTag("Inventory").SetActive(false);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].isSelected = false;
        }
    }

    void DropItem(Item item)
    {
        Transform playerTransform = GameObject.Find("Player").transform;
        Vector3 spawnTransform = playerTransform.position + playerTransform.forward;
        Instantiate(item.item, new Vector3(spawnTransform.x, spawnTransform.y + 0.2f, spawnTransform.z), Quaternion.identity);
        // inventory.rippleAmount += 10;
    }

}
