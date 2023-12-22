using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusManager : MonoBehaviour
{
    [SerializeField] public GameObject ui_load;
    [SerializeField] public Image loading_bar;
    [SerializeField] public GameObject help_menu;

    public void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void StartEasyMode() {
        StartCoroutine("AsyncStartEasyMode");
    }

    public void StartHardMode() {
        StartCoroutine("AsyncStartHardMode");
    }
    
    public void StartSurvival() {
        StartCoroutine("AsyncStartSurvival");
    }

    public void ShowHelp() {
        help_menu.SetActive(true);
    }

    public void HideHelp() {
        help_menu.SetActive(false);
    }

    public void MainMenuLoad() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void RankingLoad() {
        SceneManager.LoadScene(5, LoadSceneMode.Single);
    }

    public void SubmitNameLoad() {
        SceneManager.LoadScene(4, LoadSceneMode.Single);
    }

    public IEnumerator AsyncStartEasyMode() {
        ui_load.SetActive(true);
        AsyncOperation loading = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        while(!loading.isDone){
            loading_bar.fillAmount = loading.progress;
            yield return null;
        }
    }

    public IEnumerator AsyncStartHardMode() {
        ui_load.SetActive(true);
        AsyncOperation loading = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);

        while(!loading.isDone){
            loading_bar.fillAmount = loading.progress;
            yield return null;
        }
    }

    public IEnumerator AsyncStartSurvival() {
        ui_load.SetActive(true);
        StatsManager.instance.setStartTime();
        StatsManager.instance.resetStats();
        AsyncOperation loading = SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);

        while(!loading.isDone){
            loading_bar.fillAmount = loading.progress;
            yield return null;
        }
    }
}
