using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//Author:  Benjamin Boese
//Purpose: Making an Inventory 
//Date: 09/08/2019
//Credits:  https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec
public class UIItem : MonoBehaviour
{
    public Item item = null;
    [HideInInspector] public Image spriteImage;
    [HideInInspector] public UIItem selectedItem;
    [HideInInspector] public ToolTip toolTip;

    public void Initialize()
    {
        UpdateItem(null);
    }

    public void UpdateItem(Item item)
    {
        this.item = item;

        if(this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = item.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(this.item != null)
        {
            if(selectedItem.item != null)
            {
                Item clone = new Item(selectedItem.item);
                selectedItem.UpdateItem(this.item);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(this.item);
                UpdateItem(null);
            }
        }
        else if(selectedItem.item != null)
        {
            UpdateItem(selectedItem.item);
            selectedItem.UpdateItem(null);
        }
    }
}
