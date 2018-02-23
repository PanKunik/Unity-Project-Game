using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/Weapon")]

public class Weapon : Item {

    public int minDamage = 25;
    public int maxDamage = 30;
    public float speedAttack = 0.8f;
    public int weaponID = 0;
    public float animationID = 0;

    public override void Use()
    {
        base.Use();
        Debug.Log("Damage of item: " + minDamage + " - " + maxDamage + "\nAttack Speed: " + speedAttack);
    }

}
