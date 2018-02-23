using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class OnClicked : MonoBehaviour, IPointerClickHandler
{
    PlayerManager playerManager;
    InventoryPlayer playerInventory;
    InventorySlot slot;
    PlayerStats playerStats;
    bool unequipSlot;

    //doubleClick values
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.3f;

    // Use this for initialization
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        slot = gameObject.GetComponentInParent<InventorySlot>();
        playerInventory = InventoryPlayer.playerInventory;
        playerManager = PlayerManager.instance;
    }

    private void Update()
    {
        slot.EquipWeaponAnimation();
    }
    void DeleteLastItemFromInventory()
    {

        slot.ClearSlot();
        slot.inventoryUI.ChangeInventoryList();
    }

    void UseHPPotion()
    {
        HealthPotion healthPotion = (HealthPotion)slot.GetItem();
        switch (healthPotion.lootID)
        {
            case 9:
                Debug.Log("BigPotion used");
                if (playerStats.UseHPPotion(healthPotion.HPRegValuePerSec, healthPotion.timeToRegen))
                {
                    playerInventory.bigPotsAmount--;
                    slot.ShowAmount();
                    if (playerInventory.bigPotsAmount <= 0)
                    {
                        DeleteLastItemFromInventory();
                    }
                }
                break;
            case 10:
                Debug.Log("MiddlePotion used");
                if (playerStats.UseHPPotion(healthPotion.HPRegValuePerSec, healthPotion.timeToRegen))
                {
                    playerInventory.middlePotsAmount--;
                    slot.ShowAmount();
                    if (playerInventory.middlePotsAmount <= 0)
                    {
                        DeleteLastItemFromInventory();
                    }
                }
                break;
            case 11:
                Debug.Log("SmallPotion used");
                if (playerStats.UseHPPotion(healthPotion.HPRegValuePerSec, healthPotion.timeToRegen))
                {
                    playerInventory.smallPotsAmount--;
                    slot.ShowAmount();
                    if (playerInventory.smallPotsAmount <= 0)
                    {
                        DeleteLastItemFromInventory();
                    }
                }
                break;
        }
    }
    // Update is called once per frame
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            FastRightClickEquip();
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;

            if (clicked > 1 && Time.time - clicktime < clickdelay)
            {
                clicked = 0;
                clicktime = 0;
                OnDoubleClick();

            }
            else if (clicked > 2 || Time.time - clicktime > 1)
                clicked = 0;
        }

    }

    void OnDoubleClick()
    {
        if (slot.GetItem() && playerManager.isInShop)
        {
            if (slot.GetItem().itemID != 0)
            {
                playerInventory.rippleAmount += (int) (slot.GetItem().value / 100) * 65;
                DeleteLastItemFromInventory();
            }
            else
            {
                LootItem item = (LootItem)slot.GetItem();
                playerInventory.rippleAmount += Mathf.FloorToInt(item.value * 65 / 100);

                switch (item.lootID)
                {
                    case 0:
                        playerInventory.stoneAmount--;
                        if (playerInventory.stoneAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 1:
                        playerInventory.woodAmount--;
                        if (playerInventory.woodAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 2:
                        playerInventory.steelAmount--;
                        if (playerInventory.steelAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 3:
                        playerInventory.coalAmount--;
                        if (playerInventory.coalAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 4:
                        playerInventory.clayAmount--;
                        if (playerInventory.clayAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 5:
                        playerInventory.leatherAmount--;
                        if (playerInventory.leatherAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 6:
                        playerInventory.boneAmount--;
                        if (playerInventory.boneAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 7:
                        playerInventory.glassAmount--;
                        if (playerInventory.glassAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 8:
                        playerInventory.diamondAmount--;
                        if (playerInventory.diamondAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 9:
                        playerInventory.bigPotsAmount--;
                        if (playerInventory.bigPotsAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 10:
                        playerInventory.middlePotsAmount--;
                        if (playerInventory.middlePotsAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                    case 11:
                        playerInventory.smallPotsAmount--;
                        if (playerInventory.smallPotsAmount <= 0)
                        {
                            DeleteLastItemFromInventory();
                        }
                        break;
                }

            }
        }
    }

    void FastRightClickEquip()
    {
        if (slot.GetItem())
        {
            switch (slot.GetItem().itemID)
            {
                case 0:
                    LootItem item = (LootItem)slot.GetItem();
                    if (item.lootID >= 9 && item.lootID <= 12)
                    {
                        UseHPPotion();
                    }
                    break;
                case 1:
                    if (slot != slot.armorSlot)
                    {
                        slot.armorSlot.isSelected = true;
                        slot.OnClicked();
                    }
                    else if (slot == slot.armorSlot)
                    {
                        if (FastRightClickUnequip())
                            playerInventory.armor = null;
                    }
                    break;
                case 2:
                    if (slot != slot.helmetSlot)
                    {
                        slot.helmetSlot.isSelected = true;
                        slot.OnClicked();
                    }
                    else if (slot == slot.helmetSlot)
                    {
                        if (FastRightClickUnequip())
                            playerInventory.helmet = null;
                    }
                    break;
                case 3:
                    if (slot != slot.glovesSlot)
                    {
                        slot.glovesSlot.isSelected = true;
                        slot.OnClicked();
                    }
                    else if (slot == slot.glovesSlot)
                    {
                        if (FastRightClickUnequip())
                            playerInventory.gloves = null;
                    }
                    break;
                case 4:
                    if (slot != slot.bootsSlot)
                    {
                        slot.bootsSlot.isSelected = true;
                        slot.OnClicked();
                    }
                    else if (slot == slot.bootsSlot)
                    {
                        if (FastRightClickUnequip())
                            playerInventory.boots = null;
                    }
                    break;
                case 6:
                    if (slot != slot.weaponSlot)
                    {
                        slot.weaponSlot.isSelected = true;
                        slot.OnClicked();
                    }
                    else if (slot == slot.weaponSlot)
                    {
                        if (FastRightClickUnequip())
                            playerInventory.weapon = null;
                    }
                    
                    break;
            }
        }
    }

    bool FastRightClickUnequip()
    {
        if (playerInventory.AddToInventory(this.slot.GetItem()))
        {
            slot.ClearSlot();
            slot.inventoryUI.ChangeInventoryList();
            return true;
        }
        return false;
    }
}

