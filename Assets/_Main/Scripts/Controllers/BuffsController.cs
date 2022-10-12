using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffsController : MonoBehaviour
{
    [SerializeField] private float frozeenTimerSet;

    [SerializeField] private float igniteTimerSet;

    [SerializeField] private float igniteDamagePS;

    private float frozeenLevel;

    private float frozeenTimer;

    private float igniteTimer;

    private StatsController statsController;

    private void Awake()
    {
        statsController = GetComponent<StatsController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(frozeenTimer > 0)
        {
            frozeenTimer -= Time.deltaTime;
        }
        else if(frozeenLevel > 0) //Chequear si es optimo
        {
            statsController.SetSpeedPercentage((1/(MathF.Pow(0.9f, frozeenLevel)))/0.01f);
            frozeenLevel = 0;
        }

        if (igniteTimer > 0)
        {
            igniteTimer -= Time.deltaTime;
            statsController.TakeDamage(igniteDamagePS*Time.deltaTime);
        }
    }

    public void Frozeen()
    {
        if (frozeenLevel < 5)
        {
            frozeenLevel++;
            statsController.SetSpeedPercentage(90);
        }
        //Play Frozeen Animation
        frozeenTimer = frozeenTimerSet;
    }

    public void Ignite()
    {
        igniteTimer = igniteTimerSet;
        //play Ignite Animation
    }
 }
