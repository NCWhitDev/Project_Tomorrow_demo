using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public int health;
    public float speed;
    public int maxhealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxhealth;
    }
}
