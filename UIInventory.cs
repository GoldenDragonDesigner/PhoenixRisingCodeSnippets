using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author:  Benjamin Boese
//Purpose: Making an Inventory 
//Date: 09/08/2019
//Credits:  https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec
public class UIInventory : MonoBehaviour
{
    [Header("The Size of the Items on the canvas")]
    public List<UIItem> uIItems = new List<UIItem>();
    [Header("The slot prefab goes here")]
    public GameObject slotPrefab;
    [Header("The UI Slot Panel goes here")]
    public Transform slotPanel;
    [Header("The number of slots to instantiate")]
    public int numberOfSlots = 16;

    private void Awake()
    {
        UIItem selectedItem = GetComponentInChildren<UIItem>();
        ToolTip toolTip = GetComponentInChildren<ToolTip>();
        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab); //instantiate gameobject

            UIItem item = instance.GetComponentInChildren<UIItem>(); //get UIItem component
            item.selectedItem = selectedItem; //set instantiated item.selected = the selectedItem gameobject
            item.toolTip = toolTip; //set instantiated item.tooltip = the tooltip gameobject
            item.spriteImage = item.GetComponent<Image>(); //set the instantiated sprite image = its Image component

            item.Initialize(); // Call Initialize function on item to set object to Null

            instance.transform.SetParent(slotPanel); //Set parent = to slotPanelt
            uIItems.Add(item); // Add the instantiated Image to the list
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }
}
