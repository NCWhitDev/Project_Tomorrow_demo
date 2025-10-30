using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel; // Reference to the inventory UI panel
    public GameObject itemSlotPrefab; // Reference to the item slot prefab
    public int slotCount; // Number of slots in the inventory
    public GameObject[] itemPrefabs; // Array of item prefabs to populate the inventory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(itemSlotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform); // Instantiate the item prefab as a child of the slot
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center the item in the slot
                slot.currentItem = item; // Assign the instantiated item to the slot's currentItem
            }
        }
    }

   
}
