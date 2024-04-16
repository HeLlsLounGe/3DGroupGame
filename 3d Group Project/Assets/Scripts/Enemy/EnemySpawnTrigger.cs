using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public SpawnEnemy[] spawnEnemies;
    bool enemiesSpawned = false;

    private void Awake()
    {
        spawnEnemies = GetComponentsInChildren<SpawnEnemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !enemiesSpawned)
        {
            for (int i = 0; i < spawnEnemies.Length; i++)
            {
                spawnEnemies[i].Spawn();
            }
            enemiesSpawned=true;
        }
    }
}
