using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    [SerializeField] private float frozeenTimerSet;

    [SerializeField] private float igniteTimerSet;

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
        if(frozeenTimer >= 0)
        {
            frozeenTimer -= Time.deltaTime;
        }
        else if(frozeenTimer > 1)
        {
            frozeenTimer = 0;
        }

        if (igniteTimer >= 0)
        {
            igniteTimer -= Time.deltaTime;
        }
    }

    public void Frozeen()
    {
        if (frozeenLevel <= 5)
        {
            frozeenLevel++;
        }
        if (frozeenLevel > 0)
        {
            statsController.SetSpeedPercentage(frozeenLevel * 10);
            frozeenTimer = frozeenTimerSet;
        }
    }

    public void Ignite()
    {

    }
 }
