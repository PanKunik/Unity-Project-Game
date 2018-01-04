using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayer : Inventory {
    public static InventoryPlayer instance;
    public Item weapon;
    public Item armor;
    private void Awake()
    {

        if (instance != null)
        {
            return;
        }
        instance = this;
    }
}
