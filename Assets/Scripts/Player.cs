using UnityEngine;

public class Player : MonoBehaviour
{
    public int level;
    public int health;
    public int mana;
    public int currentMana;
    public int currentHealth;

    public int maxhealth = 100;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
        currentMana = mana;
    }


    // Dev Testing: Simulate taking damage when the 'C' key is pressed
    void Update()
    {
        // Player update logic here
        if ((Input.GetKeyDown(KeyCode.C)))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Player has died.");
        }
    }

    // ---------------------------------------------------------------------
}
