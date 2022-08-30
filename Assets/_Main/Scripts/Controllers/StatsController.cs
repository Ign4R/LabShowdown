using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    
   [SerializeField] private PlayerStats playerStats;

    //TODO: [DUDA] PONER ESTAS VARIABLES ACA O LLAMARLAS DESDE EL PLAYER MODEL?
    private float speed;

    private float jumpHeight;

    private float currentHealth;


    public float Speed { get => speed; set => speed = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }

    private void Start()
    {
        currentHealth = playerStats.MaxHealth;
        speed = playerStats.Speed;
        jumpHeight = playerStats.JumpHeight;
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
