using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIElementsManager : MonoBehaviour
{
    #region LIFE_BAR_LOGIC
    [SerializeField] private Image _lifebar;
    #endregion
    private void OnCharacterLifeChange(float currentLife, float maxLife) {
        _lifebar.fillAmount = currentLife / maxLife;
    }

    #region UNITY_EVENTS
    void Start()
    {
        Debug.Log("ARRANCANDO");
        EventsManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;

    }
    #endregion
}
