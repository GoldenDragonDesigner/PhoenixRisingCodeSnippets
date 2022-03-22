using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author:  Benjamin Boese
//Purpose: Making an Inventory 
//Date: 09/08/2019
//Credits:  https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec
public static class ItemDatabase
{
    public static List<Item> items = BuildDatabase();

    public static Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public static Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }
    public static List<Item> BuildDatabase()
    {
        List<Item> temp = new List<Item>()
        {
            new Item(0, "FullHeart", "A Heart for Healing",
            new Dictionary<string, int>
            {
                {"Healing", 1 }
            }),
            new Item(1, "Key", "For Unlocking Doors", 
            new Dictionary<string, int>
            {
                {"Unlocking", 1 }
            }),
            new Item(2, "Crystal Shard", "For Completing an Area.",
            new Dictionary<string, int>
            {
                {"Reward", 1 }
            })

        };
        return temp;
    }
}
