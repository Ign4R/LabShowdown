using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMatch : MonoBehaviour
{

    private int playerQuantity;


    private void Awake()
    {
        
    }

    private void Update()
    {
        WinCondition();
    }


   

    public void SetPlayerQuantity(int players)
    {
        playerQuantity = players;
    }

    private void WinCondition()
    {
        if (playerQuantity == 1)
            Debug.Log("Gano");
    }

    private void OnDieHandler()
    {
        playerQuantity--;
    }

    private void OnEnable()
    {
        StatsController.OnDie += OnDieHandler;
    }

    private void OnDisable()
    {
        StatsController.OnDie -= OnDieHandler;
    }
}
