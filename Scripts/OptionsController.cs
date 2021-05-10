using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        SetMusicLevel(GetMusicLevel());
        SetEffectsLevel(GetEffectsLevel());
        musicSlider.value = GetMusicLevel();
        effectsSlider.value = GetEffectsLevel();
        musicSlider.onValueChanged.AddListener(delegate { MusicSliderChange(); });
        effectsSlider.onValueChanged.AddListener(delegate { EffectsSliderChange(); });
    }

    public void SetMusicLevel(float vol)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(vol) * 20);
    }

    public void SetEffectsLevel(float vol)
    {
        mixer.SetFloat("EffectsVol", Mathf.Log10(vol) * 20);
    }

    private void MusicSliderChange() {
        PlayerPrefs.SetFloat("musicLevel", musicSlider.value);
    }

    private void EffectsSliderChange() {
        PlayerPrefs.SetFloat("effectsLevel", musicSlider.value);
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
