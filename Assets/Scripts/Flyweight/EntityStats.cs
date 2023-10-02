using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Stats/EntityStats", order = 0)]
public class EntityStats : ScriptableObject
{
    [SerializeField] private EntityStatsValues _stats;
    public float MaxHealth => _stats._maxHealth;
}

[System.Serializable]
public struct EntityStatsValues
{
    public float _maxHealth;
}