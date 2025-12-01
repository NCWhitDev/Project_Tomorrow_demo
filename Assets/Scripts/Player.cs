using UnityEngine;

public class Player : MonoBehaviour
{
    // Level, Hp and Mana
    public int level;
    public int health;
    public int mana;
    private int maxhealth = 100;
    private int maxmana = 50;

    //Stats - Strength, Intelligence, Endurance, Agility, Luck
    public int Strength;
    public int Intelligence;
    public int Endurance;
    public int Agility;
    public int Luck;


    public HealthBar healthBar;

    void Start()
    {
        health = maxhealth;
        //healthBar.SetMaxHealth(maxhealth);
        mana = maxmana;
    }

    // Setters for player stats
    public void setLevel(int lvl)
    {
        level = lvl;
    }

    public void setHealth(int hp)
    {
        health = hp;
    }

    public void setMana(int mp)
    {
        mana = mp;
    }

    public void setStrength(int str)
    {
        Strength = str;
    }

    public void setIntelligence(int intl)
    {
        Intelligence = intl;
    }

    public void setEndurance(int end)
    {
        Endurance = end;
    }

    public void setAgility(int agi)
    {
        Agility = agi;
    }

    public void setLuck(int luck)
    {
        Luck = luck;
    }

    // Getters for player stats
    public int getLevel()
    {
        return level;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMana()
    {
        return mana;
    }

    public int getStrength()
    {
        return Strength;
    }

    public int getIntelligence()
    {
        return Intelligence;
    }

    public int getEndurance()
    {
        return Endurance;
    }

    public int getAgility()
    {
        return Agility;
    }

    public int getLuck()
    {
        return Luck;
    }

    public void UpdateHealth()
    {
        //Updates health after suffering damage from a source.
        if(health == 0)
        {
            //you died :(
            Debug.Log("You died...returning to last save location");
        }
    }

    //// Dev Testing: Simulate taking damage when the 'C' key is pressed
    //void Update()
    //{
    //    // Player update logic here
    //    if ((Input.GetKeyDown(KeyCode.C)))
    //    {
    //        TakeDamage(20);
    //    }
    //}

    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //    healthBar.SetHealth(currentHealth);
    //    if (currentHealth <= 0)
    //    {
    //        Debug.Log("Player has died.");
    //    }
    //}

    // ---------------------------------------------------------------------
}
