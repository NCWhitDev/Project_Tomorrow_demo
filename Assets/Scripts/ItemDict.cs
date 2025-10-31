using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemDict : MonoBehaviour
{
    public List<Item> itemsPrefab;
    private Dictionary<int, GameObject> itemDict; // Dictionary to hold items with their IDs as keys


    /*
     * Initializes the item dictionary by populating it with item prefabs and their unique IDs.
     */
    public void Awake()
    {
        itemDict = new Dictionary<int, GameObject>();

        //Automatically populate the dictionary from the itemsPrefab list
        for (int i = 0; i < itemsPrefab.Count; i++)
        {
            if (itemsPrefab[i] != null)
            {
                itemsPrefab[i].ID = i + 1; // Assign unique ID starting from 1
            }
        }

        foreach(Item item in itemsPrefab)
        {
            itemDict[item.ID] = item.gameObject; // Add item to dictionary
        }
    }

    /*
    * Retrieves the item prefab based on its ID.
    */
    public GameObject GetItemPrefab(int itemID)
    {
       itemDict.TryGetValue(itemID, out GameObject prefab); // Try to get the item prefab from the dictionary
        if (prefab != null)
        {
            return prefab; // Return the found prefab
        }
        else
        {
            Debug.LogWarning($"Item with ID {itemID} not found.");
            return null; // Return null if not found
        }
    }


}
