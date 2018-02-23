using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : Inventory {
    public static InventoryPlayer playerInventory;
    public Item weapon;
    public Item armor;
    public Item helmet;
    public Item gloves;
    public Item boots;

    public int stoneAmount { get; set; }
    public int woodAmount { get; set; }
    public int boneAmount { get; set; }
    public int clayAmount { get; set; }
    public int coalAmount { get; set; }
    public int diamondAmount { get; set; }
    public int glassAmount { get; set; }
    public int leatherAmount { get; set; }
    public int steelAmount { get; set; }
    public int smallPotsAmount { get; set; }
    public int middlePotsAmount { get; set; }
    public int bigPotsAmount { get; set; }

    public int rippleAmount { get; set; }
    int maxRippleAmount;
    private void Awake()
    {
        stoneAmount = 0;
        woodAmount = 0;
        boneAmount = 0;
        clayAmount = 0;
        coalAmount = 0;
        diamondAmount = 0;
        glassAmount = 0;
        leatherAmount = 0;
        steelAmount = 0;
        smallPotsAmount = 0;
        middlePotsAmount = 0;
        bigPotsAmount = 0;

        rippleAmount = 0;
        maxRippleAmount = 999999;

        if (playerInventory != null)
        {
            return;
        }
        playerInventory = this;
        ChangeSize();
      
    }

    private void Update()
    {
        ChangeSize();
    }

    public override void ChangeItems(Item[] items)
    {
        base.ChangeItems(items);
    }

    void ChangeSize()
    {
        int newSize = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                newSize++;
                
            }


        }
        size = newSize;
        if (rippleAmount > maxRippleAmount)
            rippleAmount = maxRippleAmount;
    }

    public bool AddItemsToInventory(LootItem item, int itemsAmount)
    {
        if (size >= maxSize)
            return false;
        else
        {
            bool sameItemFound = false;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] is LootItem)
                {
                    LootItem lootItem = (LootItem)items[i];
                    if (items[i] && (lootItem.lootID == item.lootID))
                    {
                        sameItemFound = true;
                        switch (item.lootID)
                        {
                            case 0:
                                stoneAmount += itemsAmount;
                                return true;
                            case 1:
                                woodAmount += itemsAmount;
                                return true;
                            case 2:
                                steelAmount += itemsAmount;
                                return true;
                            case 3:
                                coalAmount += itemsAmount;
                                return true;
                            case 4:
                                clayAmount += itemsAmount;
                                return true;
                            case 5:
                                leatherAmount += itemsAmount;
                                return true;
                            case 6:
                                boneAmount += itemsAmount;
                                return true;
                            case 7:
                                glassAmount += itemsAmount;
                                return true;
                            case 8:
                                diamondAmount += itemsAmount;
                                return true;
                            case 9:
                                bigPotsAmount += itemsAmount;
                                return true;
                            case 10:
                                middlePotsAmount += itemsAmount;
                                return true;
                            case 11:
                                smallPotsAmount += itemsAmount;
                                return true;
                            case 12:
                                if ((rippleAmount + itemsAmount) < maxRippleAmount)
                                {
                                    Debug.Log("1");
                                    rippleAmount += itemsAmount;
                                    FindObjectOfType<AudioManager>().Play("PickUpCoins");
                                    return true;
                                }
                                else
                                    return false;
                            default:
                                continue;
                        }

                    }
                }
            }
            if (!sameItemFound)
            {
                switch (item.lootID)
                {
                    case 0:
                        stoneAmount += itemsAmount;
                        return AddToInventory(item);
                    case 1:
                        woodAmount += itemsAmount;
                        return AddToInventory(item);
                    case 2:
                        steelAmount += itemsAmount;
                        return AddToInventory(item);
                    case 3:
                        coalAmount += itemsAmount;
                        return AddToInventory(item);
                    case 4:
                        clayAmount += itemsAmount;
                        return AddToInventory(item);
                    case 5:
                        leatherAmount += itemsAmount;
                        return AddToInventory(item);
                    case 6:
                        boneAmount += itemsAmount;
                        return AddToInventory(item);
                    case 7:
                        glassAmount += itemsAmount;
                        return AddToInventory(item);
                    case 8:
                        diamondAmount += itemsAmount;
                        return AddToInventory(item);
                    case 9:
                        bigPotsAmount += itemsAmount;
                        return AddToInventory(item);
                    case 10:
                        middlePotsAmount += itemsAmount;
                        return AddToInventory(item);
                    case 11:
                        smallPotsAmount += itemsAmount;
                        return AddToInventory(item);
                    case 12:
                        if ((rippleAmount + itemsAmount) < maxRippleAmount)
                        {
                            rippleAmount += itemsAmount;
                            FindObjectOfType<AudioManager>().Play("PickUpCoins");
                            return true;
                        }
                        else
                            return false;
                }
            }
            return AddToInventory(item);
        }
    }

}
