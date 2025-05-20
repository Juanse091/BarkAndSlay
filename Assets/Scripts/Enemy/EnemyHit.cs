using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyHit : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 moveDirection = Vector2.left; // Cambia a Vector2.right si quieres que vaya a la derecha

    public float health;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 lastPosition;

    private bool isMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastPosition = rb.position;
        health = Random.Range(1f, 10f);
    }

    void FixedUpdate()
    {
        // Movimiento constante
        if (isMoving)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
        }

        // Calcular magnitud del movimiento
        float movementMagnitude = (rb.position - lastPosition).magnitude;

        // Cambiar animación según magnitud
        if (movementMagnitude <= 0.01f)
        {
            anim.SetFloat("magnitud", movementMagnitude); // Nombre exacto del estado de ataque
        }
        else
        {
            anim.SetFloat("magnitud", movementMagnitude); // Nombre exacto del estado de caminar
        }

        lastPosition = rb.position;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        anim.SetTrigger("hit");
        if (health <= 0)
        {
            this.tag = "Untagged";
            this.GetComponent<Collider2D>().enabled = false;
            isMoving = false;
            this.GetComponent<SpriteRenderer>().sortingOrder = -1;
            anim.SetTrigger("die");
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
