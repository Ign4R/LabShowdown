using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform positionBullet;

    [SerializeField] private float cadence;

    [SerializeField] private bool isFullAuto;

    private float cadenceTimer;

    public int ammunition { get; set; }

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

    private void Update()
    {
        if (cadenceTimer > 0)
        {
            cadenceTimer -= Time.deltaTime;
        }
    }

    private void Start()
    {
        Transform = transform;
    }

    public void Attack()
    {
        if (cadenceTimer <= 0 || isFullAuto == false)
        {
            Instantiate(bullet, positionBullet.position, transform.rotation);
            if (isFullAuto == true)
            {
                cadenceTimer = 1 / cadence;
            }
        }
    }
}
