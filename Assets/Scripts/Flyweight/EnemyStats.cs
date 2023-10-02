using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats", order = 0)]

public class EnemyStats : EntityStats
{
    [SerializeField] private EnemyStatsValues _enemyStats;
    public float Damage => _enemyStats._damage;
}

[System.Serializable]
public struct EnemyStatsValues
{
    public float _damage;
}
