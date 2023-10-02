using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStats", menuName = "Stats/CharacterStats", order = 0)]

public class CharacterStats : EntityStats
{
    [SerializeField] private CharacterStatsValues _characterStats;
    public float MovementSpeed => _characterStats._movementSpeed;
    public float JumpHeight => _characterStats._jumpHeight;
}

[System.Serializable]
public struct CharacterStatsValues
{
    public float _movementSpeed;
    public float _jumpHeight;
}
