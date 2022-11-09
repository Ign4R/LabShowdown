using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<StatsController>()?.TakeDamage(damage);
    }
}
