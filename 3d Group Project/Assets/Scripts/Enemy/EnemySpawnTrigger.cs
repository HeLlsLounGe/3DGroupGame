using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public SpawnEnemy[] spawnEnemies;
    [SerializeField] bool delayedWave;
    
    public float waveTimer;
    bool enemiesSpawned = false;
    [SerializeField]
    
    WinFunction winFunction;
    GameObject brain;

    private void Awake()
    {
        brain = GameObject.FindGameObjectWithTag("EnemyBrain");
        spawnEnemies = GetComponentsInChildren<SpawnEnemy>();
        winFunction = brain.GetComponent<WinFunction>();
        winFunction.Spawners++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !enemiesSpawned)
        {
            if (delayedWave == false)
            {
                for (int i = 0; i < spawnEnemies.Length; i++)
                {
                    spawnEnemies[i].Spawn();
                }
                enemiesSpawned = true;
                winFunction.Spawners--;
            }

            if (delayedWave == true)
            { Invoke("DelayedWave", waveTimer); }
        }
    }

    void DelayedWave()
    {
        for (int i = 0; i < spawnEnemies.Length; i++)
        {
            spawnEnemies[i].Spawn();
        }
        enemiesSpawned = true;
        winFunction.Spawners--;
    }
}
