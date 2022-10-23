using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsController : MonoBehaviour
{
    
   [SerializeField] private PlayerStats playerStats;

    //TODO: [DUDA] PONER ESTAS VARIABLES ACA O LLAMARLAS DESDE EL PLAYER MODEL?
    private float speed;

    private float jumpHeight;

    private float currentHealth;

    private float maxHealth;

    private int lifes;

    private PlayerController playerController;
    private PlayerView playerView;

    public static event Action<int> OnDie;

    public static event Action<int, int> OnLifeDecrese;

    public static event Action<int> OnRespawn;

    public static event Action OnDamage;


    public float Speed { get => speed; set => speed = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerView = GetComponent<PlayerView>();
    }

    private void Start()
    {
        currentHealth = playerStats.MaxHealth;
        maxHealth = playerStats.MaxHealth;
        speed = playerStats.Speed;
        jumpHeight = playerStats.JumpHeight;
    }
    public void TakeDamage(float damage)
    {     
        currentHealth -= damage;
        playerView.TakeDamageAnim();
        if (currentHealth <= 0 && lifes == 1)
        {
            currentHealth = 0;
            Die();
            OnDie?.Invoke(playerController.PlayerConfig.PlayerIndex);
        }
        if (currentHealth <= 0 && lifes > 0)
        {
            lifes--;
            OnLifeDecrese?.Invoke(playerController.PlayerConfig.PlayerIndex, lifes);
            currentHealth = maxHealth;
            OnRespawn?.Invoke(playerController.PlayerConfig.PlayerIndex);
        }

    }
    public void Die()
    {
        print($"Die {gameObject.name}");
        Destroy(gameObject);
    }

    public float SetSpeedPercentage(float percentage)
    {
        speed = speed * (percentage * 0.01f);
        return Speed;
    }

    public void SetLifes(int lifes)
    {
        this.lifes = lifes;
    }

}
