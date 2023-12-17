using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenusManager : MonoBehaviour
{
    public GameObject ui_load;
    public Image loading_bar;

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

    public void MainMenuLoad() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void RankingLoad() {
        SceneManager.LoadScene(6, LoadSceneMode.Single);
    }

    public void SubmitNameLoad() {
        SceneManager.LoadScene(7, LoadSceneMode.Single);
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
        AsyncOperation loading = SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);

        while(!loading.isDone){
            loading_bar.fillAmount = loading.progress;
            yield return null;
        }
    }

    public IEnumerator AsyncStartSurvival() {
        ui_load.SetActive(true);
        StatsManager.instance.setStartTime();
        AsyncOperation loading = SceneManager.LoadSceneAsync(5, LoadSceneMode.Single);

        while(!loading.isDone){
            loading_bar.fillAmount = loading.progress;
            yield return null;
        }
    }
}
