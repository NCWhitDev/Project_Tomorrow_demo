using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{

    private InventoryController inventoryController; // Reference to the InventoryController
    private GameObject nearbyItem; // Reference to the nearby item
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get reference to InventoryController component in the scene
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    void Update()
    {
        // Check input each frame while we have a nearby item
        if (nearbyItem != null && Input.GetKeyDown(KeyCode.E))
        {
            Item itemComp = nearbyItem.GetComponent<Item>(); // Get the Item component
            if (itemComp == null)
            {
                Debug.LogWarning("Nearby object tagged 'Item' has no Item component: " + nearbyItem.name);
                nearbyItem = null;
                return;
            }

            Debug.Log("PlayerItemCollector picked up: " + itemComp.Name);

            if (inventoryController == null)
            {
                Debug.LogError("InventoryController not found in scene.");
                return;
            }

            bool itemAdded = inventoryController.AddItemToInventory(nearbyItem);
            if (itemAdded)
            {
                itemComp.PickUp();
                Destroy(nearbyItem);
                nearbyItem = null;
            }
        }
    }

    // Detect when the player enters the trigger collider of an item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Debug.Log("PlayerItemCollector detected a collision with: " + collision.gameObject.name);
            // store the item so Update() can handle the key press
            nearbyItem = collision.gameObject;
        }
    }

    // Detect when the player exits the trigger collider of an item
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == nearbyItem)
        {
            nearbyItem = null; // clear the reference when exiting the trigger
        }
    }

    // https://www.youtube.com/watch?v=qQ0ECbgaAKk&list=PLaaFfzxy_80HtVvBnpK_IjSC8_Y9AOhuP&index=11
    // OLD CODE FOR REFERENCE: TriggerEnter2D method that immediately picks up the item
    //if (collision.gameObject.CompareTag("Item"))
    //{
    //    Item item = collision.gameObject.GetComponent<Item>();
    //    if (item != null)
    //    {
    //        // Attempt to add the item to the inventory
    //        bool itemAdded = inventoryController.AddItemToInventory(collision.gameObject);

    //        if (itemAdded)
    //        {
    //            item.PickUp(); // From ItemPickUpController.cs --> Call the PickUp method to handle pickup logic and UI
    //            // If the item was successfully added to the inventory, destroy the item in the world
    //            Destroy(collision.gameObject);
    //    }
    //        }
    //}


}
