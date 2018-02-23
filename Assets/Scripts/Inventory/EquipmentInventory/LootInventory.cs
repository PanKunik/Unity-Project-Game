using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInventory : Inventory {
    public static LootInventory lootInventory;
    private void Awake()
    {

        if (lootInventory != null)
        {
            return;
        }
        lootInventory = this;
    }

}
