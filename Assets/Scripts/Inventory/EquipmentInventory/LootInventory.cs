using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInventory : Inventory {
    public static LootInventory instance;
    private void Awake()
    {

        if (instance != null)
        {
            return;
        }
        instance = this;
    }

}
