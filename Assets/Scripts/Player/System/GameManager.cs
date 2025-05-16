using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [SerializeField] private Text Counter;
    [SerializeField] private GameObject HitBox;
    public Animator anim;
    private int counter;

    public void OnClicked()
    {
        counter += 1;
        UpdateCounter();
        PlayerHit();
    }

    public void UpdateCounter()
    {
        Counter.text = counter.ToString();
    }

    public void PlayerHit()
    {
        anim.SetTrigger("Attack");
    }
}
