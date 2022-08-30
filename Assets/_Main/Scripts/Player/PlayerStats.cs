using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float Speed { get => speed; set => speed = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
}
