using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, IWeapon
{
    public Transform Transform { get; set; }

    public Collider2D Collider2D { get; set; }

    float hitTimer; // Este timer es temporal, hay que quitarlo

    [SerializeField] private float hitTimerSet;
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
        if 
    }

    public void Attack()
    {
        if (hitTimer <= 0)
        {
            //Activar animacion de ataque cuerpo-cuerpo
            GetComponent<Collider2D>().enabled = true;
            hitTimer = hitTimerSet;
        }
    }
}
