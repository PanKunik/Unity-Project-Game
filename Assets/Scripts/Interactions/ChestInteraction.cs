using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestInteraction : Interactable
{

    public Transform itemsParent;
    public LootInventory inventory;
    public InventorySlot[] slots { get; set; }
    public GameObject lootInventory;
    public GameObject playerInventory { get; set; }
    bool isOpen = false;
    LootInventory currInventory;
    LootInventory emptyInventory;
    Animator anim;
    LootInventoryUI inventoryUI;

    void Awake()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Inventory");
        emptyInventory = GameObject.FindGameObjectWithTag("EmptyInventory").GetComponent<LootInventory>();
        currInventory = emptyInventory;
        anim = GetComponent<Animator>();
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        lootInventory.SetActive(false);
    }

    void SetProperties()
    {
        anim = GetComponent<Animator>();
        inventoryUI = lootInventory.GetComponent<LootInventoryUI>();
        currInventory = inventory;
        LootInventory.lootInventory = currInventory;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

    }

    void ZeroProperties()
    {
        currInventory = emptyInventory;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
            slots[i].isSelected = false;
        }
        lootInventory.SetActive(false);
        playerInventory.SetActive(false);
    }

    new void Update()
    {
        base.Update();
        isOpen = GetIsFocused();

        if (!isOpen && currInventory != emptyInventory)
        {

            anim.SetBool("IsOpen", isOpen);
            ZeroProperties();

        }
        else
        {
            anim.SetBool("IsOpen", isOpen);
        }

    }

    public override void Interact()
    {
        base.Interact();
        SetProperties();
        playerInventory.SetActive(true);
        lootInventory.SetActive(true);
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < currInventory.items.Count)
            {
                slots[i].AddItem(currInventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }
    }
}
