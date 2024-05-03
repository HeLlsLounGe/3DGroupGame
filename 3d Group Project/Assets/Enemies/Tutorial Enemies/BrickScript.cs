using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    EnemyHealthBar healthBar;
    void Start()
    {
        healthBar = GetComponent<EnemyHealthBar>();
        healthBar.TakeDamage(500);
    }
}

