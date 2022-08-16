using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    private Rigidbody2D rb;

    [SerializeField] private int damage;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();
        
    }

    private void Move()
    {
        
        //rb.velocity = new Vector3(speed, rb.velocity.y, 0f);
        rb.velocity = transform.right * speed;
        //transform.position = new Vector3(transform.position.x + (Time.deltaTime * speed), transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<HealthController>()?.TakeDamage(damage);
    }
        
}
