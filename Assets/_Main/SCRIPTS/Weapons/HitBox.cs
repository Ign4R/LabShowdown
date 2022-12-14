using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private Collider2D colliderPlayer;

    [SerializeField] private Collider2D _collider;

    [SerializeField] private SpriteRenderer spriteRen;


    private void Start()
    {
        //colliderPlayer = GetComponentInParent<Collider2D>();
        //_collider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(colliderPlayer, _collider, true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("hit");
        AudioManager.Instance.Play("fist");
        collision.GetComponent<StatsController>()?.TakeDamage(damage);
    }

    public void OffRenderFists(bool render)
    {
        if (render==false) spriteRen.enabled = false;
        else spriteRen.enabled = true;
    }
}
