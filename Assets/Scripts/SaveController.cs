using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{

    private string savelocation;

    /* 
     * Initializes the save location path when the game starts.
     */
    void Start()
    {
        savelocation = Path.Combine(Application.persistentDataPath, "saves.json");
        print("Save location: " + savelocation);
        LoadGame();
    }


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
            mapBoundary = UnityEngine.Object.FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.gameObject.name 
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
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            SaveGame(); // Create a new save file if none exists
            print("New save file created at: " + savelocation);
        }
    }
}
