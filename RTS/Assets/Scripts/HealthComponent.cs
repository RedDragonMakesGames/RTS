using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int defaultMaxHealth = 100;
    public int maxHealth;
    [SerializeField] private int health;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = defaultMaxHealth;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
    }
}
