using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int level;
    [SerializeField] public int health;
    [SerializeField] public int speed;
    private int currentHealth;

    public bool isDead = false;
    public bool IsDead() => isDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = health;
    }

    public int getSpeed() => speed;

    public int getHealth() => currentHealth;

    public void UpdateHealth(int n)
    {
        currentHealth += n; //damage or healing
        //Updates health after suffering damage from a source.
        Debug.Log("Enemy health updated by " + n + ". Current health: " + currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log($"{name} died.");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}
