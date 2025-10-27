using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public int playerLevel; //Player's current level
    public int playerHealth; //Player's current health
    public string[] inventoryItems; //Array to hold inventory item names
    public string mapBoundary; //The name of the boundary the player is currently in

}
