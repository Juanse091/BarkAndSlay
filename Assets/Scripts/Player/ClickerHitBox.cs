using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerHitBox : MonoBehaviour
{

    public int damage = 1;

    void Start()
    {
        // Cambiar daño
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<EnemyHit>()?.TakeDamage(damage);
        }
    }

}
