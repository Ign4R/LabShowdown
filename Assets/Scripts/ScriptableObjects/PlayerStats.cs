using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] public float health;
    [SerializeField] public float speed;
    [SerializeField] public float jumpHeight;
}
