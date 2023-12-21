using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;
    public int EnemyKills => enemyKills;
    public int CoinsPicked => coinsPicked;
    public int HitsTaken => hitsTaken;

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
        Debug.Log("Kill: " + enemyKills);
    }

    public void addCoinsPicked() {
        coinsPicked++;
        Debug.Log("Coins Picked: " + coinsPicked);
    }

    public void addHitsTaken() {
        hitsTaken++;
        Debug.Log("Hits Taken: " + hitsTaken);
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

    public void resetStats() {
        enemyKills = 0;
        coinsPicked = 0;
        hitsTaken = 0;
    }

    private void Start() {
        _db = new Database();
    }
}
