using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item/HealthPotion")]
public class HealthPotion : LootItem 
{
    public int HPRegValuePerSec;
    public float timeToRegen;

}
