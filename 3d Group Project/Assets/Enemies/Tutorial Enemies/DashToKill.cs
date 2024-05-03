using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToKill : MonoBehaviour
{
    EnemyHealthBar healthScript;
    float healTime = 0.1f;
    float healthTimer;
    void Start()
    {
        healthScript = GetComponent<EnemyHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
