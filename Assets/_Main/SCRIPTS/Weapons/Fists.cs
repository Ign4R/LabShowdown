using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : MonoBehaviour
{
    [SerializeField] private float hitTimerSet;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject hitBoxR;

    [SerializeField] private GameObject hitBoxL;

    private bool lasAtackR;

    float hitTimer; // Este timer es temporal, hay que quitarlo

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
        else
        {
            if (lasAtackR == true) hitBoxR.GetComponent<Collider2D>().enabled = false;
            else hitBoxL.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Attack()
    {
        if (hitTimer <= 0)
        {
            if (lasAtackR == false)
            {
                animator.SetTrigger("AttackR");
                hitBoxR.GetComponent<Collider2D>().enabled = true;
                hitTimer = hitTimerSet;
                lasAtackR = true;
            }
            else
            {
                animator.SetTrigger("AttackL");
                hitBoxL.GetComponent<Collider2D>().enabled = true;
                hitTimer = hitTimerSet;
                lasAtackR = false;
            }
        }
    }
}
