using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int level;
    [SerializeField] public int health;
    [SerializeField] public int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public int getSpeed()
    {
        return speed;
    }

    public int getHealth()
    {
        return health;
    }

    public void UpdateHealth()
    {
        //Updates health after suffering damage from a source.
        if (health == 0)
        {
            //you died :(
            Debug.Log("Enemy dead, deleting unit.");
        }
    }
}
