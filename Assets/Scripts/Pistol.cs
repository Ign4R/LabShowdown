using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public Transform Transform { get; set; }

    public Collider2D Collider2D { get; set; }

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        Transform = transform;
        
    }

    
    public void Attack()
    {
        throw new System.NotImplementedException();
    }

}
