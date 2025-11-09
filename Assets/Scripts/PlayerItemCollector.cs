using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{

    private InventoryController inventoryController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                // Attempt to add the item to the inventory
                bool itemAdded = inventoryController.AddItemToInventory(collision.gameObject);

                if (itemAdded)
                {
                    // If the item was successfully added to the inventory, destroy the item in the world
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
