using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : MonoBehaviour
{
    [SerializeField] private float hitTimerSet;

    [SerializeField] private Animator animator;
    public HitBox hitBox { get; private set; }

    private bool lasAtackR;

    float hitTimer; // Este timer es temporal, hay que quitarlo

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hitBox = GetComponentInChildren<HitBox>();
    }
    private void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        print("Attack fists");
        if (hitTimer <= 0)
        {
            if (lasAtackR == false)
            {
                animator.SetTrigger("AttackR");
                hitTimer = hitTimerSet;
                lasAtackR = true;
            }
            else
            {
                animator.SetTrigger("AttackL");
                hitTimer = hitTimerSet;
                lasAtackR = false;
            }
        }
    }

   
}
