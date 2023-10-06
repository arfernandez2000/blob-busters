using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIElementsManager : MonoBehaviour
{
    #region LIFE_BAR_LOGIC
    [SerializeField] private Image _lifebar;
    [SerializeField] private Image _manaBar;
    #endregion
    private void OnCharacterLifeChange(float currentLife, float maxLife) {
        Debug.Log("cambiando el fillAmount");
        Debug.Log(currentLife);
        Debug.Log(maxLife);
        Debug.Log(_lifebar.fillAmount);
        _lifebar.fillAmount = currentLife / maxLife;
        Debug.Log(_lifebar.fillAmount);
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
