using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text Counter;
    [SerializeField] private GameObject HitBox;

    [Header("Animators")]
    public Animator animPlayer;
    public Animator animClicker;

    [Header("Audio")]

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip golpeNormal;
    [SerializeField] private AudioClip macheteHit;
    [SerializeField] private AudioClip zombieHit;
    [SerializeField] private AudioClip xpSound;

    [HideInInspector] public int XP;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnClicked()
    {
        PlayerHit();
    }

    public void UpdateCounter()
    {
        audioSource.PlayOneShot(xpSound, volumeScale: 0.25f);
        XP += 1;
        Counter.text = XP.ToString();
    }

    public void PlayerHit()
    {
        animClicker.SetTrigger("Click");
        animPlayer.SetTrigger("Attack");

    }

    public void AudioHit()
    {
        audioSource.PlayOneShot(golpeNormal);
    }

    public static implicit operator GameObject(GameManager v)
    {
        throw new NotImplementedException();
    }


    public void MacheteHit()
    {
        audioSource.PlayOneShot(macheteHit);
    }   

    public void ZombieHit()
    {
        audioSource.PlayOneShot(zombieHit);
    }

}
