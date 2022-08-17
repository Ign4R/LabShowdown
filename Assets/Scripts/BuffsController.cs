using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffsController : MonoBehaviour
{
    [SerializeField] private float setIgniteTimer;

    private int frozenLevel;
    private float fronzenTimer;
    private float igniteTimer;
    private float igniteDamage;
    void Start()
    {
        
    }

    void Update()
    {

        if (igniteTimer > 0)
        {
            igniteTimer -= Time.deltaTime;
        }
    }

    public void Frozen()
    {

        if (frozenLevel < 6)
        {
            frozenLevel ++;
            // Efecto (se agregara cuando es ten las Stats) LCH 17-08
            /*player.StatsController.SetSpeed(-0.1 * fronzenLevel * baseSpeed);
            if (fronzenLvel == 6)
            {

            }*/
        }
    }
    public void Ignite()
    {
        //PlayerController.LifeController.GetDamage(igniteDamage)
    }
}
