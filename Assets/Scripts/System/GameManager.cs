using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text Counter;
    [SerializeField] private GameObject HitBox;
    public Animator animPlayer;
    public Animator animClicker;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip golpeNormal;
    [SerializeField] private AudioClip golpeFuerte;

    private int counter;
    private int golpeCount = 0;

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
        counter += 1;
        Counter.text = counter.ToString();
    }

    public void PlayerHit()
    {
        animClicker.SetTrigger("Click");
        animPlayer.SetTrigger("Attack");

        golpeCount++;

        if (golpeCount >= 3)
        {
            audioSource.PlayOneShot(golpeFuerte);
            golpeCount = 0;
        }
        else
        {
            audioSource.PlayOneShot(golpeNormal);
        }
    }

    public static implicit operator GameObject(GameManager v)
    {
        throw new NotImplementedException();
    }
}
