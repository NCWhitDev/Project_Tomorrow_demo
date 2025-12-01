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

    //Run-time references
    private int currentHealth;
    private int currentMana;

    public HealthBar healthBar;

    private void Awake()
    {
        currentHealth = maxhealth;
        currentMana = maxmana;
    }

    void Start()
    {
        currentHealth = health;
        currentMana = mana;

        if(healthBar != null)
        {
            healthBar.SetMaxHealth(maxhealth);
            healthBar.SetHealth(currentHealth);
        }
    }


    // Getters for player stats
    public int getLevel() => level;
    public int getHealth() => currentHealth;
    public int getMana() => currentMana;
    public int getStrength() => Strength;
    public int getIntelligence() => Intelligence;
    public int getEndurance() => Endurance;
    public int getAgility() => Agility;
    public int getLuck() => Luck;

    // Setters for player stats
    public void setLevel(int lvl)           => level = lvl;
    public void setHealth(int hp)           =>  health = hp;
    public void setMana(int mp)             => mana = mp;
    public void setStrength(int str)        => Strength = str;
    public void setIntelligence(int intel)  => Intelligence = intel;
    public void setEndurance(int end)       => Endurance = end;
    public void setAgility(int agi)         => Agility = agi;
    public void setLuck(int luck)           => Luck = luck;

    public void UpdateHealth(int n)
    {
        currentHealth = currentHealth + n;

        if(healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player has been defeated! Returning to last saved location");
            Die();
        }
    }

    private void Die()
    {
        // Implement respawn here
        //Destroy(gameObject);
        Debug.Log("Player died.");
    }

}
