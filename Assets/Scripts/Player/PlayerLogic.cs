using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip golpeNormal;


    public void AudioHit()
    {
        audioSource.PlayOneShot(golpeNormal);
    }
}
