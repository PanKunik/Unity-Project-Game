using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopMenuSlot : MonoBehaviour
{

    public Item item;
    public Image image;
    Text price;
    public void Awake()
    {
        image.sprite = item.icon;
        image.enabled = true;
        price = gameObject.GetComponentInChildren<Text>();

        
    }

    public void ClearSlot()
    {
        item = null;
        image.sprite = null;
        image.enabled = false;
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            item = newItem;

            image.sprite = item.icon;
            image.enabled = true;
        }
        else
        {
            item = null;
            image.enabled = false;
        }
    }

    public void Update()
    {
        ShowPrice();
    }

    private void ShowPrice()
    {
        if (this.item)
        {
            price.enabled = true;
            price.text = item.value.ToString();

        }
        else
        {
            price.enabled = false;
        }
    }
}
