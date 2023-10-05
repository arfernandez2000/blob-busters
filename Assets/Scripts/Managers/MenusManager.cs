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

    public void StartGame() {
        StartCoroutine("AsyncStart");
    }

    public void MainMenuLoad() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public IEnumerator AsyncStart() {
        ui_load.SetActive(true);
        AsyncOperation loading = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

        while(!loading.isDone){
            loading_bar.fillAmount = loading.progress;
            yield return null;
        }
    }
}
