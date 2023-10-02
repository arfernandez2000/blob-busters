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
        Debug.Log("EVENT MANAGER!!!!");
        if (instance != null) {
            Debug.Log("DESTROYING EVENT MANAGER");
            Destroy(this);
        }
        instance = this;
    }

    #endregion

    #region GAME_MANAGER_ACTIONS
    public event Action<bool> OnGameOver;
    public void EventGameOver(bool isVictory) {
        if (OnGameOver != null) OnGameOver(isVictory);
    }
    #endregion

    #region UI_ELEMENTS
    public event Action<float, float> OnCharacterLifeChange;

    public void CharacterLifeChange(float currentLife, float maxLife) {
        Debug.Log("VALORES");
        Debug.Log(currentLife);
        Debug.Log(maxLife);
        Debug.Log(OnCharacterLifeChange.GetInvocationList().Length);
        if (OnCharacterLifeChange == null) {
            Debug.Log("ES NULO");
        }
        if (OnCharacterLifeChange != null) {
            Debug.Log("NO ES NULO");
            OnCharacterLifeChange(currentLife, maxLife); 
        }
    }
    #endregion
}
