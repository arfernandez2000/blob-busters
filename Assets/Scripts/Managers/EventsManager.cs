using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;
    
    #region UNITY_EVENTS

    private void Awake() {
        Debug.Log("EVENT MANAGER!!!!");
        if (instance != null) {
            Debug.Log("DESTROYING EVENT MANAGER");
            Destroy(this);
        }
        instance = this;
    }

    private void Start() {
        
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
    public event Action<float, float> OnSpellCast;

    public void CharacterLifeChange(float currentLife, float maxLife) {
        OnCharacterLifeChange?.Invoke(currentLife, maxLife);
    }

    public void SpellCast(float currentMana, float maxMana) {
        OnSpellCast?.Invoke(currentMana, maxMana);
    }
    #endregion

    #region SOUNDS
    public event Action<AudioSource> OnSoundEffect;

    public void SoundEffect(AudioSource audio) {
        Debug.Log("Entro al coin sound effect del event manager");
        if(OnSoundEffect != null)
            OnSoundEffect(audio);
        else
            Debug.Log("ES FUCKING NULL");
    }

    #endregion

}
