using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadNameInput : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private InputField _inputField;

    private void Start() {
        _button.onClick.AddListener(GetInputOnClickHandler);
    }

    public void GetInputOnClickHandler() {
        string playerName = _inputField.text;
        if (string.IsNullOrEmpty(playerName) || playerName == "Name") {
            Debug.Log("Player name is empty");
        } else {
            string score = StatsManager.instance.getSurvivalGameTime();
            Debug.Log($"Score: {score}");
            StatsManager.instance.db.AddRankingRecord(new RankingModel(0, playerName, score));
            SceneManager.LoadScene(5, LoadSceneMode.Single);
        }
        
    }
}
