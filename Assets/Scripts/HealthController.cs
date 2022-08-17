using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    private int currentHealth;

    [SerializeField]
    private int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

    }
    public void Die()
    {
        print($"Die {gameObject.name}");
        Destroy(gameObject);
    }
}
