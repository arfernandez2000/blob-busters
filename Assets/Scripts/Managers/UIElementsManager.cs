using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIElementsManager : MonoBehaviour
{
    #region LIFE_BAR_LOGIC
    [SerializeField] private Image _lifebar;
    [SerializeField] private Image _manaBar;
    #endregion
    private void OnCharacterLifeChange(float currentLife, float maxLife) {
        _lifebar.fillAmount = currentLife / maxLife;
    }

    private void OnSpellCast(float currentMana, float maxMana) {
        _manaBar.fillAmount = currentMana / maxMana;
    }

    #region UNITY_EVENTS
    void Start() {
        EventsManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
        EventsManager.instance.OnSpellCast += OnSpellCast;
    }
    #endregion
}
