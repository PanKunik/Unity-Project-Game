using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTraderInteract : NPCInteract
{
    public Transform itemsParent;
    public LootInventory inventory { get; set; }
    public ShopMenuSlot[] slots { get; set; }
    public GameObject playerInventory { get; set; }
    public GameObject shopMenu;
    Animator anim;
    bool isInteracting = false;
    LootInventory currInventory;
    LootInventory emptyInventory;
    // Use this for initialization
    void Awake()
    {
        base.Start();
        emptyInventory = GameObject.FindGameObjectWithTag("EmptyInventory").GetComponent<LootInventory>();
        currInventory = emptyInventory;
        inventory = gameObject.GetComponent<LootInventory>();
        slots = itemsParent.GetComponentsInChildren<ShopMenuSlot>();
        anim = gameObject.GetComponent<Animator>();
        playerInventory = GameObject.FindGameObjectWithTag("Inventory");
        //shopMenu = GameObject.Find("ShopMenuUI");
        shopMenu.SetActive(false);
    }

    void SetProperties()
    {
       
        //inventoryUI = shopMenu.GetComponent<LootInventoryUI>();
        currInventory = inventory;
        LootInventory.lootInventory = currInventory;
        slots = itemsParent.GetComponentsInChildren<ShopMenuSlot>();

    }

    void ZeroProperties()
    {
        currInventory = emptyInventory;
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].ClearSlot();
        }
        shopMenu.SetActive(false);
        playerInventory.SetActive(false);
        playerManager.isInShop = false;
    }


    public override void Interact()
    {
        base.Interact();
        playerInventory.SetActive(true);
        shopMenu.SetActive(true);
        playerManager.isInShop = true;
        anim.SetTrigger("Interact");
        SetProperties();
        UpdateUI();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        if (!GetIsFocused() && currInventory != emptyInventory)
        {
            ZeroProperties();
            
        }
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
