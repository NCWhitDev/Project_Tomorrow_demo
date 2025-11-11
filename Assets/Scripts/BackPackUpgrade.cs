using UnityEngine;
public class BackPackUpgarde : MonoBehaviour
{
    // Backpack default slot count is 5
    InventoryController slotCount;

    void Awake()
    {
        // Try to find the InventoryController in the scene if not assigned
        if (slotCount == null)
            slotCount = FindFirstObjectByType<InventoryController>();

        if (slotCount == null)
        {
            Debug.LogWarning("BackPackUpgrade: InventoryController not found. Using default behavior (no direct SlotCounter modifications).");
            return;
        }

        if (slotCount.slotCount == 0)
        {
            Debug.Log("Backpack size not set, initializing to default size.");
            slotCount.slotCount = 5;
        }

        if (slotCount.slotCount == 27)
        {
            Debug.Log("Can't upgrade, Backpack is at maximum capacity.");
        }
    }

    public int bagUpgrade()
    {
        if (slotCount == null)
        {
            Debug.LogWarning("BackPackUpgrade: InventoryController not found. Returning default check 1.");
            return 1; // Or some default value indicating failure
        }

        if (slotCount.slotCount == 5)
        {
            //Tier 1 upgrade
            return slotCount.slotCount = 10; ;
        }
        else if (slotCount.slotCount == 10)
        {
            //Tier 2 upgrade
            return slotCount.slotCount = 15;
            
        }
        else if (slotCount.slotCount == 15)
        {
            //Tier 3 upgrade
            return slotCount.slotCount = 20;
           
        }
        else if (slotCount.slotCount == 20)
        {
            //Tier 4 upgrade
           return slotCount.slotCount = 27; //Max upgrade
        }
        else
        {
            return slotCount.slotCount;
        }
    }
}
