using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIElement : MonoBehaviour
{
    [SerializeField] private Text _enemyKills;
    [SerializeField] private Text _coinsPicked;
    [SerializeField] private Text _hitsTaken;

    private void Start() {
        _enemyKills.text = StatsManager.instance.EnemyKills.ToString();
        _coinsPicked.text = StatsManager.instance.CoinsPicked.ToString();
        _hitsTaken.text = StatsManager.instance.HitsTaken.ToString();

    }
}
