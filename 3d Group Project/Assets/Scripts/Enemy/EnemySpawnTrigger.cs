using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public SpawnEnemy[] spawnEnemies;
    public bool multipleWaves;
    public float waveTimer;
    bool enemiesSpawned = false;
    [SerializeField]
    bool firstWave = true;
    
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
            if (firstWave)
            {
                for (int i = 0; i < spawnEnemies.Length; i++)
                {
                    spawnEnemies[i].Spawn();
                }
                enemiesSpawned = true;
            }

            winFunction.Spawners--;

            if (multipleWaves == true)
            { Invoke("NextWave", waveTimer); }
        }
    }

    void NextWave()
    {
        for (int i = 0; i < spawnEnemies.Length; i++)
        {
            spawnEnemies[i].Spawn();
        }
        enemiesSpawned = true;
    }
}
