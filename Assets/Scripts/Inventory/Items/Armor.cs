using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Armor")]

public class Armor : Item {

    public int armor;

    public override void Use()
    {
        base.Use();
        Debug.Log("Armor: " + armor);
    }
}
