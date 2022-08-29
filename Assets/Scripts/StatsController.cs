using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public float Speed { get => this.speed;}

    public float JumpHeight { get => this.jumpHeight;}

    [SerializeField] private float maxHealth;

    [SerializeField] private float speed;

    [SerializeField] private float jumpHeight;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
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
