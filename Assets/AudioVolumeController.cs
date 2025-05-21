using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider volumeSlider;

    [Header("Exposed Parameter Names")]
    public string musicParam = "MusicVolume";
    public string sfxParam = "SFXVolume";

    void Start()
    {
        // Cargar valor guardado o usar 0.75 por defecto
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);

        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Convertir de valor 0-1 a escala logarítmica
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;

        mixer.SetFloat(musicParam, dB);
        mixer.SetFloat(sfxParam, dB);

        PlayerPrefs.SetFloat("MasterVolume", value);
    }
}
