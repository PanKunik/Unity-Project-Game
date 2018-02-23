using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetBtnActive : MonoBehaviour, IPointerClickHandler
{
    Button btn;
    Color enabledTextColor;
    Color disabledTextColor;
    ShopMenuSlot slot;
    InventoryPlayer inventoryPlayer;

    //doubleclick values
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.3f;

    // Use this for initialization
    void Start () {
        slot = gameObject.GetComponentInParent<ShopMenuSlot>();
        inventoryPlayer = InventoryPlayer.playerInventory;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;

            if (clicked > 1 && Time.time - clicktime < clickdelay)
            {
                clicked = 0;
                clicktime = 0;
                OnDoubleClick();

            }
            else if (clicked > 2 || Time.time - clicktime > 1)
                clicked = 0;
        }
    }

    void OnDoubleClick()
    {
        if (inventoryPlayer.size < inventoryPlayer.maxSize)
        {
            if (slot.item.itemID != 0)
            {
                if (inventoryPlayer.rippleAmount >= slot.item.value)
                {
                    inventoryPlayer.AddToInventory(slot.item);
                    inventoryPlayer.rippleAmount -= slot.item.value;
                }
            }
            else
            {
                if (inventoryPlayer.rippleAmount >= slot.item.value)
                {
                    LootItem item = (LootItem)slot.item;
                    inventoryPlayer.AddItemsToInventory(item, 1);
                    inventoryPlayer.rippleAmount -= slot.item.value;
                }
            }
        }
    }
    
}

