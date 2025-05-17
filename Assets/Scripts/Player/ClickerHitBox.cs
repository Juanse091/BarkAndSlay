using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerHitBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            GameManager.Instance.UpdateCounter();
        }
    }

}
