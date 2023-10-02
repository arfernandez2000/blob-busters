using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusManager : MonoBehaviour
{

    public void Start() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void MainMenuLoad() {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
