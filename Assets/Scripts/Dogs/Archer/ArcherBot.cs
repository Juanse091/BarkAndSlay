using UnityEngine;

public class ArcherBot : MonoBehaviour
{
    public float detectionRadius = 5f;
    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;
    public Transform firePoint;
    public GameObject arrowPrefab;
    public float fireRate = 1.5f;

    private float nextFireTime = 0f;
    private Transform currentTarget;
    private Animator animator;
    private AudioSource audioSource;


    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);

            foreach (var enemy in enemies)
            {
                Vector2 direction = (enemy.transform.position - firePoint.position).normalized;
                Debug.DrawRay(firePoint.position, direction * detectionRadius, Color.green, 0.1f);

                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, detectionRadius, enemyLayer | obstacleLayer);

                if (hit.collider != null && hit.collider.CompareTag("enemy"))
                {
                    currentTarget = enemy.transform;
                    animator.SetTrigger("Shoot"); // <-- activa animación
                    nextFireTime = Time.time + fireRate;
                    break;
                }
            }
        }
    }

    public void ShootArrow()
    {
        if (currentTarget == null) return;

        Vector2 direction = (currentTarget.position - firePoint.position).normalized;
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetDirection(direction);

        audioSource.Play();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
