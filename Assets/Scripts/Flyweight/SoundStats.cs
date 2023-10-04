using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundStats", menuName = "Stats/SoundStats", order = 0)]
public class SoundStats : ScriptableObject
{
    [SerializeField] private SoundStatsValues _soundStats;
    public AudioClip Coin => _soundStats._coin;
}

[System.Serializable]
public struct SoundStatsValues
{
    public AudioClip _coin;
}