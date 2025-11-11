using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDict itemDict; // Reference to the ItemDict component
    public GameObject inventoryPanel; // Reference to the inventory UI panel
    public GameObject itemSlotPrefab; // Reference to the item slot prefab
    public int slotCount; // Number of slots in the inventory
    public int upgradeTier; // Current upgrade tier of the inventory
    public GameObject[] itemPrefabs; // Array of item prefabs to populate the inventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDict = FindFirstObjectByType<ItemDict>(); // Find the ItemDict component in the scene
        
        //for (int i = 0; i < slotCount; i++)
        //{
        //    Slot slot = Instantiate(itemSlotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //    if (i < itemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(itemPrefabs[i], slot.transform); // Instantiate the item prefab as a child of the slot
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center the item in the slot
        //        slot.currentItem = item; // Assign the instantiated item to the slot's currentItem
        //    }
        //}

    }

    public bool AddItemToInventory(GameObject itemPrefab)
    {
        //Look for empty slot
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null) //If our slot is free and is not occupied.
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform); // Instantiate the item prefab as a child of the slot
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center the item in the slot
                slot.currentItem = newItem; // Assign the instantiated item to the slot's currentItem
                return true; // Item added successfully
            }
        }
        Debug.Log("Inventory Full!");
        return false; // Inventory full, item not added
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null) // Check if the slot has an item
            {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex()});  // GetSiblingIndex returns the index of the slot in the inventory panel
            }
        }
        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        foreach(Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject); // Clear existing slots 
        }

        for(int i = 0; i < slotCount; i++)
        {
            Instantiate(itemSlotPrefab, inventoryPanel.transform); // Recreate empty slots
        }

        // Populate inventory with saved items
        foreach(InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>(); // Get the slot at the saved index
                GameObject itemPrefab = itemDict.GetItemPrefab(data.itemID); // Get the item prefab from the ItemDict using the saved item ID

                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform); // Instantiate the item prefab as a child of the slot
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center the item in the slot
                    slot.currentItem = item; // Assign the instantiated item to the slot's currentItem
                }
            }
        }
    }

    public int bagUpgrade()
    {
        if (slotCount == 27)
        {
            Debug.Log("Can't upgrade, Tier 4 Backpack is at maximum capacity.");
        }

        // Upgrade logic
        if (slotCount == 0)
        {
            Debug.LogWarning("BackPackUpgrade: InventoryController not found. Returning default check 1.");
            return 1; // Or some default value indicating failure
        }

        if (upgradeTier == 0)
        {
            //Tier 1 upgrade
            upgradeTier = 1;
            return slotCount = 10;
        }
        else if (upgradeTier == 1)
        {
            //Tier 2 upgrade
            upgradeTier = 2;
            return slotCount = 15;

        }
        else if (upgradeTier == 2)
        {
            //Tier 3 upgrade
            upgradeTier = 3;
            return slotCount = 20;

        }
        else if (upgradeTier == 3)
        {
            //Tier 4 upgrade
            upgradeTier = 4;
            return slotCount = 27; //Max upgrade
        }
        else
        {
            return slotCount;
        }
    }

    public int GetUpgradeTier()
    {
        return upgradeTier;
    }
}
