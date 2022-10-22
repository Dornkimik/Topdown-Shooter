using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy")]
public class EnemySO : ScriptableObject
{
    public Color color;

    public int health;
    public float speed;
    public float attackDamage;
    [Range(0, 1)] public float chanceToSpawn;
}
