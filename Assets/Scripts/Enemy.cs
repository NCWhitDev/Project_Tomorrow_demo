using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int level;
    [SerializeField] public int health;
    [SerializeField] public int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setHealth(health);
        setSpeed(speed);
        setLevel(level);
    }

    public void setLevel(int level)
    {
        this.level = level;
    }
    public void setSpeed(int spd)
    {
        speed = spd;
    }

    public void setHealth(int hp)
    {
        health = hp;
    }

    public int getSpeed()
    {
        return speed;
    }

    public int getHealth()
    {
        return health;
    }
}
