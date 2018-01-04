using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemPickup : Interactable {

    public Item item;
    public override void Interact()
    {
        base.Interact();

        PickUp();
    }


    void PickUp()
    {
       
        if (item is Weapon)
        {
            Weapon weapon = (Weapon)item;
            Debug.Log("Pickuping weapon " + weapon);
            AddToInv(weapon);

        }
        else if(item is Armor)
        {
            Armor armor = (Armor)item;
            Debug.Log("Pickuping armor " + armor);
            AddToInv(armor);
        }
        else if (item is Item)
        {
            AddToInv(item);
            Debug.Log("Pickuping item " + item);
        }

    }

    void AddToInv(Item item)
    {
        bool wasPickuped = InventoryPlayer.instance.AddToInventory(item);
        if (wasPickuped)
            Destroy(gameObject);
    }

}
