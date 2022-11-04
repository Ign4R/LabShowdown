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
    void Update()
    {
        if (statsController.ResetEffects)
        {
            frozeenTimer = 0f;
            igniteTimer = 0f;
        }
        else
        {
            if (frozeenTimer >= 1)
            {
                frozeenTimer -= Time.deltaTime;
            }
            else if (frozeenLevel >= 1) //Chequear si es optimo
            {
                statsController.SetSpeedPercentage((1 / (MathF.Pow(0.9f, frozeenLevel))) / 0.01f);
                frozeenLevel = 0;
            }

            if (igniteTimer >= 1)
            {
                igniteTimer -= Time.deltaTime;
                statsController.TakeDamage(igniteDamagePS * Time.deltaTime);
            }
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
