using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour, IWeapon
{
    public Rigidbody2D Rigidbody2D { get; set; }

    public Transform _Transform { get; set; }

    public int Ammo { get; set; }

    public Collider2D _Collider2D { get; set; }

    public SpriteRenderer _SpriteRenderer { get; set; }

    public bool CanDestroy => throw new System.NotImplementedException();

    public float CurrentTime => throw new System.NotImplementedException();

    public Physics2D _Physics2D => throw new System.NotImplementedException();

    public bool TouchGround => throw new System.NotImplementedException();

    public GameObject GO => throw new System.NotImplementedException();

    float hitTimer; // Este timer es temporal, hay que quitarlo

    [SerializeField] private float hitTimerSet;
    private void Awake()
    {
        _Collider2D = GetComponent<Collider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _Transform = transform;
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
        Destroy(gameObject);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

}
