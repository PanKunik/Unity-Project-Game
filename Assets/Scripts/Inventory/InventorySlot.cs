using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {
    Item item;
    InventoryUI inventoryUI;
    public Image icon;
    public bool isSelected =false;
    public Image selected;
    public Item GetItem()
    {
        return item;
    }
    private void Awake()
    {
        inventoryUI = GameObject.Find("Inventory").GetComponent<InventoryUI>();
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;

            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            item = null;
            icon.enabled = false;
        }
    }
    private void Update()
    {
        selected.enabled = isSelected;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnClicked()
    {
        isSelected = !isSelected;
        for(int i = 0; i < inventoryUI.slots.Length; i++)
        {
            
            if (inventoryUI.slots[i].isSelected && inventoryUI.slots[i] != this)
            {
                if (inventoryUI.slots[i] == null)
                {
                    Item temp = inventoryUI.slots[i].item;
                    inventoryUI.slots[i].AddItem(this.item);
                    this.AddItem(temp);
                }
                else if (this == null)
                {
                    Item temp = this.item;
                    this.AddItem(inventoryUI.slots[i].item);
                    inventoryUI.slots[i].AddItem(temp);
                }
                else
                {
                    Debug.Log(i);
                    Item temp = this.item;
                    this.AddItem(inventoryUI.slots[i].item);
                    inventoryUI.slots[i].AddItem(temp);
                    inventoryUI.slots[i].isSelected = false;
                    isSelected = false;
                    inventoryUI.ChangeInventoryList();
                    //this.ClearSlot();
                }

            }
          


        }
            //selected.enabled = !selected.enabled;
    }

    private Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = this.transform.position;
        
        if (item)
        {
            icon.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item)
        {
            icon.transform.position = eventData.position;
        }
     
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
            this.transform.position = startPosition;
    }
}
