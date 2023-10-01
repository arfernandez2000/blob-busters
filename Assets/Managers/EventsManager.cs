using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;
    
    #region UNITY_EVENTS

    private void Awake() {

    }

    private void Start() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    #endregion

    #region GAME_MANAGER_ACTIONS
    public event Action<bool> OnGameOver;
    public void EventGameOver(bool isVictory) {
        if (OnGameOver != null) OnGameOver(isVictory);
    }
    #endregion
}
