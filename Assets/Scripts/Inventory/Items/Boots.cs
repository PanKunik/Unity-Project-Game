using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Boots")]

public class Boots : Armor {

    public float movementSpeed = 1.0f;

    public override void Use()
    {
        base.Use();
        Debug.Log("Movement Speed: " + movementSpeed);
    }
	
}
