using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public int playerLevel; //Player's current level
    public int playerHealth; //Player's current health
    public List<InventorySaveData> inventorySaveData; //List of items in the player's inventory
    public string mapBoundary; //The name of the boundary the player is currently in
}
