using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text counterText; // HUD principal
    [SerializeField] private Text xpText;      // Canvas de mejoras

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

    [Header("Efectos")]
    [SerializeField] private GameObject bloodSplashPrefab;
    [SerializeField] private Transform splashSpawnPoint;

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

        counterText.text = XP.ToString(); // HUD contador
        xpText.text = "XP    " + XP;         // Canvas de mejoras
    }

    public int GetXP()
    {
        return XP;
    }

    public bool TrySpendXP(int amount)
    {
        if (XP >= amount)
        {
            XP -= amount;

            counterText.text = XP.ToString();
            xpText.text = "XP    "+XP;

            return true;
        }
        return false;
    }

    public void PlayerHit()
    {
        animClicker.SetTrigger("Click");
        animPlayer.SetTrigger("Attack");

        Instantiate(bloodSplashPrefab, splashSpawnPoint.position, Quaternion.identity);
    }

    public void AudioHit()
    {
        audioSource.PlayOneShot(golpeNormal);
    }

    public void MacheteHit()
    {
        audioSource.PlayOneShot(macheteHit);
    }

    public void ZombieHit()
    {
        audioSource.PlayOneShot(zombieHit);
    }

    public static implicit operator GameObject(GameManager v)
    {
        throw new NotImplementedException();
    }
}
