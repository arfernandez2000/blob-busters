using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    #region PRIVATE_PROPERTIES
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float _healthRecovery = 10;
    [SerializeField] private MeshRenderer _meshRenderer;
    #endregion

    #region UNITY_EVENTS
    void Start() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Coin picked up");
            EventsManager.instance.SoundEffect(audioSource.clip);
            _meshRenderer.enabled = false;
            transform.gameObject.GetComponent<Collider>().enabled = false;
            Destroy(gameObject);
        }
    }
    #endregion
}
