using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    #region Unity_Events
    // Start is called before the first frame update
    void Start()
    {
        EventsManager.instance.OnSoundEffect += OnSoundEffect;
    }
    #endregion

    #region EVENT_ACTIONS
    private void OnSoundEffect(AudioSource audio)
    {
        Debug.Log("En Sound Effect Manager: " + audio.clip);
        Debug.Log(audio == null);
        audio.PlayOneShot(audio.clip);
    }
    #endregion
}
