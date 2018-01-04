using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Weapon")]

public class Weapon : Item {

    public int damage = 10;
    public float speedAttack = 3;
    public int weaponID = 0;

    public override void Use()
    {
        base.Use();
        Debug.Log("Damage of item: " + damage + "\nAttack Speed: " + speedAttack);
    }

}
