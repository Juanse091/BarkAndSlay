using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyHit : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 moveDirection = Vector2.left; // Cambia a Vector2.right si quieres que vaya a la derecha

    public int health;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 lastPosition;

    private int XPCounter;

    [SerializeField] private GameObject[] XPPrefabs;
    private bool isMoving = true;
    private bool xpDropped = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastPosition = rb.position;
        health = Mathf.RoundToInt(Random.Range(1f, 10f));
        XPCounter = health;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
        }

        float movementMagnitude = (rb.position - lastPosition).magnitude;

        if (movementMagnitude <= 0.01f)
        {
            anim.SetFloat("magnitud", movementMagnitude);
        }
        else
        {
            anim.SetFloat("magnitud", movementMagnitude);
        }

        lastPosition = rb.position;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        anim.SetTrigger("hit");
        if (health <= 0)
        {
            GameManager.Instance.ZombieHit();

            this.tag = "Untagged";
            this.GetComponent<Collider2D>().enabled = false;
            isMoving = false;
            this.GetComponent<SpriteRenderer>().sortingOrder = -1;
            DropXP();
            anim.SetTrigger("die");
        }
    }

    private void DropXP()
    {
        if (xpDropped) return;
        xpDropped = true;

        for (int i = 0; i < XPCounter; i++)
        {
            int randomIndex = Random.Range(0, XPPrefabs.Length);
            GameObject xpPrefab = XPPrefabs[randomIndex];
            Vector2 randomPosition = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            Instantiate(xpPrefab, rb.position + randomPosition, Quaternion.identity);
        }
    }


    private void Die()
    {
        Destroy(gameObject);
    }
}
