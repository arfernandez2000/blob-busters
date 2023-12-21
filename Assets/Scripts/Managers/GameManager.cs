using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool _isVictory = false;
    [SerializeField] private GameObject _blob;
    [SerializeField] private GameObject _coin;
    // [SerializeField] private Text _gameOverMessage;

    #region UNITY_EVENTS
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.OnGameOver += OnGameOver;

        InvokeRepeating("InstantiateNewBlob", 3.0f, 4.0f);
        InvokeRepeating("InstantiateNewCoin", 3.0f, 4.0f);
    }
    #endregion

    #region ACTIONS
    private void OnGameOver(bool isVictory) {
        _isGameOver = true;
        _isVictory = isVictory;

        // If active scene is Survival, log
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Survival") {
            StatsManager.instance.setEndTime();
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene(_isVictory? 6:7, LoadSceneMode.Single);
        }

    }
    #endregion

    private void InstantiateNewBlob() {
        Terrain terrain = FindObjectOfType<Terrain>();
        
        float margin = 80f;

        float xMin = terrain.transform.position.x + margin;
        float zMin = terrain.transform.position.z + margin;

        float xMax = xMin + terrain.terrainData.size.x - 2 * margin;
        float zMax = zMin + terrain.terrainData.size.z - 2 * margin;

        Vector3 randomPos = new Vector3(
            Random.Range(xMin,xMax),
            0,
            Random.Range(zMin,zMax)
        );

        float heightOffset = 4;
        randomPos.y = Terrain.activeTerrain.SampleHeight(randomPos) + heightOffset;

        GameObject newBlob = Instantiate(_blob, randomPos, Quaternion.identity);
    }

    private void InstantiateNewCoin() {
        Terrain terrain = FindObjectOfType<Terrain>();
        
        float xMin = terrain.transform.position.x;
        float zMin = terrain.transform.position.z;

        float xMax = xMin + terrain.terrainData.size.x;
        float zMax = zMin + terrain.terrainData.size.z;

        Vector3 randomPos = new Vector3(
            Random.Range(xMin,xMax),
            0,
            Random.Range(zMin,zMax)
        );

        float heightOffset = 2;
        randomPos.y = Terrain.activeTerrain.SampleHeight(randomPos) + heightOffset;

        GameObject newBlob = Instantiate(_coin, randomPos, Quaternion.identity);
    }

}
