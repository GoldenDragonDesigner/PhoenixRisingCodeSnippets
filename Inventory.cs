using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Purpose: Making an Inventory 
//Date: 09/08/2019
//Credits:  https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec
public class Inventory : MonoBehaviour
{
    public List<Item> characterItems = new List<Item>();
    [Header("The Inventory Game Object goes here")]
    public UIInventory inventoryUI;

    public void Start()
    {
        
        //GiveItem(1);
        //GiveItem(0);
        //RemoveItem(1);
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        //}
    }

    public void GiveItem(int id)
    {
        Item itemToAdd = ItemDatabase.GetItem(id);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public void GiveItem(string itemName)
    {
        Item itemToAdd = ItemDatabase.GetItem(itemName);
        characterItems.Add(itemToAdd);
        inventoryUI.AddNewItem(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item CheckForItem(int id)
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id)
    {
        Item itemToRemove = CheckForItem(id);
        if(itemToRemove != null)
        {
            characterItems.Remove(itemToRemove);
            inventoryUI.RemoveItem(itemToRemove);
            Debug.Log("Item removed: " + itemToRemove.title);
        }
    }
}
