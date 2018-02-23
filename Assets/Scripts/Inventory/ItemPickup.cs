using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPickup : Interactable {
    
    public Item item;
    public int amountItemPickup;
    GameObject HUDCanvas;
    public GameObject nameItem;

    private void Awake()
    {
        HUDCanvas = GameObject.Find("HUDCanvas");
        nameItem = HUDCanvas.transform.GetChild(1).gameObject;
    }

    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void OnMouseOver()
    {
        if (nameItem && (nameItem.activeSelf == false) && PauseMenu.IsGamePaused == false)
        {
            nameItem.SetActive(true);
            nameItem.transform.position = Input.mousePosition;
            string count = amountItemPickup > 1 ? " (" + amountItemPickup + ")" : "" ;
            nameItem.GetComponent<TextMeshProUGUI>().text = item.name + count;
            // nameItem.GetComponent<TextMeshPro>().text = item.name + count;
        }
    }

    private void OnMouseExit()
    {
        if (nameItem && nameItem.activeSelf)
            nameItem.SetActive(false);
    }

    void PickUp()
    {
        if (InventoryPlayer.playerInventory.size < InventoryPlayer.playerInventory.maxSize)
        {
            if (item is Weapon)
            {
                Weapon weapon = (Weapon)item;
                Debug.Log("Pickuping weapon " + weapon);
                AddToInv(weapon);
                FindObjectOfType<AudioManager>().Play("PickUpMats");

            }
            else if (item is Armor)
            {
                Armor armor = (Armor)item;
                Debug.Log("Pickuping armor " + armor);
                AddToInv(armor);
                FindObjectOfType<AudioManager>().Play("PickUpMats");
            }
            else if (item is Boots)
            {
                Boots boots = (Boots)item;
                Debug.Log("Pickuping armor " + boots);
                AddToInv(boots);
                FindObjectOfType<AudioManager>().Play("PickUpMats");
            }
            else if (item is LootItem)
            {
                LootItem lootItem = (LootItem)item;
                Debug.Log("Pickuping: " + lootItem.name);
                AddItems(lootItem);
                FindObjectOfType<AudioManager>().Play("PickUpMats");

            }
            else if (item is Item)
            {
                AddToInv(item);
                Debug.Log("Pickuping item " + item);
                FindObjectOfType<AudioManager>().Play("PickUpMats");
            }
            if(nameItem)
                nameItem.SetActive(false);

        }
    }

    void AddToInv(Item item)
    {
       
        bool wasPickuped = InventoryPlayer.playerInventory.AddToInventory(item);
        if (wasPickuped)
            Destroy(gameObject);
        
    }

    void AddItems(LootItem item)
    {
        bool wasPickuped = InventoryPlayer.playerInventory.AddItemsToInventory(item, amountItemPickup);
        if (wasPickuped)
            Destroy(gameObject);
        
    }
}
