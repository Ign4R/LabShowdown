using System;
using UnityEngine;
 public class Katana: MonoBehaviour, IWeapon
 {
    public Transform Transform { get; set; }

    public Collider2D Collider2D { get; set; }

    public Rigidbody2D Rigidbody2D { get; set; }
    public LayerMask WeaponLayerMask { get; set; }

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

    }

    public void Attack()
    {
        Hit();
    }

    private void Hit()
    {
        
    }
 }

