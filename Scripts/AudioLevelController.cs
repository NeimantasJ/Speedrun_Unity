using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioLevelController : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    void Start()
    {
        SetMusicLevel(GetMusicLevel());
        SetEffectsLevel(GetEffectsLevel());
    }

    public void SetMusicLevel(float vol)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(vol) * 20);
    }

    public void SetEffectsLevel(float vol)
    {
        mixer.SetFloat("EffectsVol", Mathf.Log10(vol) * 20);
    }

    private float GetMusicLevel()
    {
        return PlayerPrefs.GetFloat("musicLevel");
    }

    private float GetEffectsLevel()
    {
        return PlayerPrefs.GetFloat("effectsLevel");
    }
}
