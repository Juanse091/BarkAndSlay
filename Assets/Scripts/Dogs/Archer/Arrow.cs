using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;

    public float damage = 1f;
    private Vector2 direction;



    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            collision.GetComponent<EnemyHit>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
