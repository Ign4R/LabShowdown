using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, IWeapon
{
    public Transform Transform { get; set; }

    public Collider2D Collider2D { get; set; }

    float hitTimer; // Este timer es temporal, hay que quitarlo
    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Transform = transform;
    }
    private void Update()
    {
       /* if (hitTimer < 0)
        {
            Collider2D.enabled = false;
        }
        else
        {
            Collider2D.enabled = true;
            hitTimer -= Time.deltaTime;
        }*/
    }

    public void Attack()
    {
        Hit();
    }

    private void Hit()
    {
        //Collider2D.enabled = true;
        HitTimerSet();
        //Cambiar por evento del animator
    }

    private void HitTimerSet()
    {
        hitTimer = 1;
    }
}
