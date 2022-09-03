using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon 
{
    Transform Transform { get; set; }
    Collider2D Collider2D { get; }
    Rigidbody2D Rigidbody2D { get; }
    void Attack();
}
