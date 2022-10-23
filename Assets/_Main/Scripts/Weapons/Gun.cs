using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform positionBullet;
    [SerializeField] private int ammo;
    [SerializeField] private bool canLifeTime;

    [SerializeField] private float cadence;

    private float cadenceTimer;

    public Transform Transform { get; set; }

    public Collider2D Collider2D { get; set; }

    public Rigidbody2D Rigidbody2D { get; set; }

    public SpriteRenderer SpriteRenderer { get; set; }
    public int Ammo { get => ammo; private set => ammo = value; }

    public bool CanLifeTime { get => canLifeTime; private set => canLifeTime = value; }

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (cadenceTimer > 0)
        {
            cadenceTimer -= Time.deltaTime;
        }
    }

    private void Start()
    {
        canLifeTime = true;
        Transform = transform;
    }

    public void Attack()
    {
        if (cadenceTimer <= 0)
        {
            Instantiate(bullet, positionBullet.position, transform.rotation);
            ammo--;
            cadenceTimer = 1 / cadence;

        }
    }

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
