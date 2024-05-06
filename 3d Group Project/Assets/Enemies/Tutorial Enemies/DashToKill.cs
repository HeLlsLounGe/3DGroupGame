using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashToKill : MonoBehaviour
{
    EnemyHealthBar healthScript;
    float healTime = 0.3f;
    float healthTimer;
    void Start()
    {
        healthScript = GetComponent<EnemyHealthBar>();
        healthTimer = healTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthTimer > 0)
        {
            healthTimer = healthTimer - Time.deltaTime;
        }
        else if (healthTimer <= 0)
        {
            healthTimer = healTime;
            if (healthScript.hitpoints > 0)
            {
                healthScript.hitpoints = healthScript.MaxHP;
            }
        }
    }
}
