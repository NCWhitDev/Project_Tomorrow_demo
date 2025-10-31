using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    private string savelocation;
    private InventoryController inventoryController;
    /* 
     * Initializes the save location path when the game starts.
     */
    void Start()
    {
        savelocation = Path.Combine(Application.persistentDataPath, "saves.json");
        inventoryController = FindFirstObjectByType<InventoryController>();
        print("Save location: " + savelocation);
        LoadGame();
    }

    //private IEnumerator Start() // <-- Changed from void to IEnumerator
    //{
    //    savelocation = Path.Combine(Application.persistentDataPath, "saveData.json");
    //    inventoryController = FindFirstObjectByType<InventoryController>();

    //    // Wait 1 frame to make sure InventoryController has fully initialized and slots are created
    //    yield return null;

    //    LoadGame();
    //}

    /*
     * Saves the current game state to a JSON file.
     * playerPosition: The current position of the player in the game world.
     * mapBoundary: The name of the current map boundary the player is in.
     */
    public void SaveGame()
   {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,                                 
            mapBoundary = UnityEngine.Object.FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.gameObject.name, 
            inventorySaveData = inventoryController.GetInventoryItems() ?? new List<InventorySaveData>()  // Get current inventory items, So we check if the inventory is null, we add a new list of data
        };
        File.WriteAllText(savelocation, JsonUtility.ToJson(saveData)); // Serialize and write to file
        print("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(savelocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(savelocation)); // Reads and deserialize from file
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition; // Restore player position
            FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<BoxCollider2D>(); // Restore map boundary

            inventoryController.SetInventoryItems(saveData.inventorySaveData); // Restore inventory items
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            SaveGame(); // Create a new save file if none exists
            inventoryController.SetInventoryItems(new List<InventorySaveData>());
            print("New save file created at: " + savelocation);
        }
    }
}
