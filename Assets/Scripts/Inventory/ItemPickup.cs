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
        Debug.Log("Pickuping item " + item.name);
        //Add to inventory
        bool wasPickuped = Inventory.instance.AddToInventory(item);
        if(wasPickuped)
            StartCoroutine(StartDelay());
        
    }
    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
