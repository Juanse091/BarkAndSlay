using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacheteLogic : MonoBehaviour
{
    public float detectionRange = 1.5f;
    public LayerMask enemyLayer;

    public float baseAttackDelay = 1f;
    public float attackDelayUpgradeMultiplier = -0.1f;
    private float nextAttackTime = 0f;

    private Animator anim;
    public int upgradeLevel = 0;

    public int macheteDamage = 1; 

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, detectionRange, enemyLayer);

        if (hit.collider != null && Time.time >= nextAttackTime)
        {
            anim.SetTrigger("attack");

            EnemyHit enemy = hit.collider.GetComponent<EnemyHit>();
            if (enemy != null)
            {
                enemy.TakeDamage(macheteDamage);
            }

            nextAttackTime = Time.time + GetCurrentAttackDelay();
        }
    }

    public void UpgradeAttack()
    {
        upgradeLevel++;
    }

    private float GetCurrentAttackDelay()
    {
        float delay = baseAttackDelay + (upgradeLevel * attackDelayUpgradeMultiplier);
        return Mathf.Max(0.1f, delay);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * detectionRange);
    }
}
