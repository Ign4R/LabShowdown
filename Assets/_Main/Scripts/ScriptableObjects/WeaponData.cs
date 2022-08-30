using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] public float fireRate;
    [SerializeField] public float damage;
    [SerializeField] public float ammo;
    [SerializeField] public Sprite weaponSprite;

}
