using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsController : MonoBehaviour
{
    
   [SerializeField] private PlayerStats playerStats;

    public float CurrentHealth { get; private set; }

    private float maxHealth;

    private int lifes;

    public bool ResetEffects { get; private set; }

    private PlayerController playerController;
    private PlayerView playerView;

    public static event Action<int> OnDie;

    public static event Action<int,int> OnLivesDecrese;

    public static event Action<int,float,float> OnUpdateHealth;

    public static event Action<int> OnRespawn;


    public float Speed { get; set; }
    public float JumpHeight { get; set; }

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerView = GetComponent<PlayerView>();
    }

    private void Start()
    {
        CurrentHealth = playerStats.MaxHealth;
        maxHealth = playerStats.MaxHealth;
        Speed = playerStats.Speed;
        JumpHeight = playerStats.JumpHeight;
    }
    private void Update()
    {
       
    }
    public void TakeDamage(float damage)
    {
        ResetEffects = false;
        CurrentHealth -= damage;
        OnUpdateHealth?.Invoke(playerController.PlayerConfig.PlayerIndex, CurrentHealth, maxHealth);
        playerView.Anim.SetTrigger("Hit");
        if (CurrentHealth <= 0 && lifes == 1)
        {
            CurrentHealth = 0;
            Die();
            OnDie?.Invoke(playerController.PlayerConfig.PlayerIndex);
        }
        if (CurrentHealth <= 0 && lifes > 0)
        {
            ResetEffects = true;
            lifes--;
            OnLivesDecrese?.Invoke(playerController.PlayerConfig.PlayerIndex, lifes);
            CurrentHealth = maxHealth;
            OnUpdateHealth?.Invoke(playerController.PlayerConfig.PlayerIndex, CurrentHealth, maxHealth);
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
        Speed = Speed * (percentage * 0.01f);
        return Speed;
    }

    public void SetLifes(int lifes)
    {
        this.lifes = lifes;
    }

}
