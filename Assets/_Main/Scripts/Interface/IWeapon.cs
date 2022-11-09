using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    Transform _Transform { get; set; }
    Collider2D _Collider2D { get; }
    Rigidbody2D Rigidbody2D { get; }

    SpriteRenderer _SpriteRenderer { get; set; }

    GameObject GO { get; }
    int Ammo { get; }
    bool CanDestroy { get; }
    bool IsFullAuto { get; }
    bool TouchGround { get; }
    float CurrentTime { get; }
    void Attack();
    void DestroyWeapon();

    void OnTriggerEnter2D(Collider2D collision);
}
