using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] XPPrefabs;

    public float spawnInterval = 5f;   // Tiempo entre spawns (mejorable)
    public int xpAmount = 1;           // Cuánto XP lanza cada vez (mejorable)

    private float nextSpawnTime = 0f;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnXP();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnXP()
    {
        for (int i = 0; i < xpAmount; i++)
        {
            int randomIndex = Random.Range(0, XPPrefabs.Length);
            GameObject xpPrefab = XPPrefabs[randomIndex];
            Vector2 offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            Instantiate(xpPrefab, transform.position + (Vector3)offset, Quaternion.identity);
        }

        Debug.Log("XP Dropped: " + xpAmount);
    }

    // Llamado desde upgrades para mejorar
    public void UpgradeSpawnRate(float reduction)
    {
        spawnInterval = Mathf.Max(0.5f, spawnInterval - reduction); // Nunca menos de 0.5s
    }

    public void UpgradeXPAmount(int extra)
    {
        xpAmount += extra;
    }
}
