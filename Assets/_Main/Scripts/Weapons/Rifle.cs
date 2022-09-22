using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform positionBullet;
    public Transform Transform { get; set; }

    public Collider2D Collider2D { get; set; }

    public Rigidbody2D Rigidbody2D { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }

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

    
    public void Attack()
    {
        Instantiate(bullet, positionBullet.position, transform.rotation);
    }

}
