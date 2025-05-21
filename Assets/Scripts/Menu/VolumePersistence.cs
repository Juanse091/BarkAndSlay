using UnityEngine;
using UnityEngine.Audio;

public class VolumePersistence : MonoBehaviour
{
    public AudioMixer mixer;
    public string musicParam = "MusicVolume";
    public string sfxParam = "SFXVolume";

    void Start()
    {
        if (PlayerPrefs.HasKey(musicParam))
        {
            float musicValue = PlayerPrefs.GetFloat(musicParam);
            mixer.SetFloat(musicParam, Mathf.Log10(musicValue) * 20);
        }

        if (PlayerPrefs.HasKey(sfxParam))
        {
            float sfxValue = PlayerPrefs.GetFloat(sfxParam);
            mixer.SetFloat(sfxParam, Mathf.Log10(sfxValue) * 20);
        }
    }
}
