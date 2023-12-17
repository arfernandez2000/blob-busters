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
    private static DateTime startTime;
    private static DateTime endTime;

    public Database db => _db;
    private Database _db;

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

    public void setStartTime() {
        startTime = DateTime.Now;
    }

    public void setEndTime() {
        endTime = DateTime.Now;
    }

    public string getSurvivalGameTime() {
        TimeSpan timeSpent = endTime - startTime;
        return string.Format("{0:00}:{1:00}:{2:00}", timeSpent.Hours, timeSpent.Minutes, timeSpent.Seconds);
    }

    private void Start() {
        _db = new Database();
    }
}
