using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectManager : MonoBehaviour
{
    private AudioSource _audioSource;
    #region UNITY_EVENTS
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.OnSoundEffect += OnSoundEffect;
        _audioSource = GetComponent<AudioSource>();
    }
    #endregion

    #region EVENT_ACTIONS
    private void OnSoundEffect(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
    #endregion
}
