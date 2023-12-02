using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    private static int enemyKills = 0;
    private static int coinsPicked = 0;
    private static int hitsTaken = 0;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }

    public void addEnemyKill() {
        enemyKills++;
        EventsManager.instance.KillCountChange(enemyKills);
    }

    public void addCoinsPicked() {
        coinsPicked++;
    }

    public void addHitsTaken() {
        hitsTaken++;
    }

    private void Start() {}
}
