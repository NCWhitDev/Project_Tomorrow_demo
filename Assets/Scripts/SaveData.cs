using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;
    public int playerLevel; //Player's current level
    public int playerHealth; //Player's current health
    public int playerMana; //Player's current mana
    public int playerStrength; //Player's strength stat
    public int playerIntelligence; //Player's intelligence stat
    public int playerEndurance; //Player's endurance stat
    public int playerAgility; //Player's agility stat
    public int playerLuck; //Player's luck stat
    public int upgradeTier; //Player's inventory upgrade tier
    public List<InventorySaveData> inventorySaveData; //List of items in the player's inventory
    public string mapBoundary; //The name of the boundary the player is currently in
}
