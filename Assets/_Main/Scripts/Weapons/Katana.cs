using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, IWeapon
{
    public Rigidbody2D Rigidbody2D { get; set; }

    public Transform Transform { get; set; }

    public int Ammo { get; set; }

    public Collider2D Collider2D { get; set; }

    public SpriteRenderer SpriteRenderer { get; set; }

    float hitTimer; // Este timer es temporal, hay que quitarlo

    [SerializeField] private float hitTimerSet;
    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Transform = transform;
    }
    private void Update()
    {
        
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

    public void DestroyWeapon()
    {
        throw new System.NotImplementedException();
    }
}
