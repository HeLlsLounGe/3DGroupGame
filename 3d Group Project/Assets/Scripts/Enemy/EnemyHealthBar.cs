using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitpoints = 50;
    private int MaxHP;
    //Slider healthSlider;
    public bool isDead = false;
    public int randomPoint;
    StateMachine stateMachine;
    Enemy enemy;
    EnemyAttackScript enemyAttackScript;
    //public Canvas healthBar;

    private void Awake()
    {
        //healthSlider = GetComponentInChildren<Slider>();
        MaxHP = hitpoints;
        //healthSlider.maxValue = MaxHP;
        //healthSlider.value = hitpoints;
        //healthBar.enabled = false;

        randomPoint = Random.Range(0, 6);
   
    }

    public void TakeDamage(int dmg)
    {
        hitpoints -= dmg;
        //if (hitpoints == healthSlider.maxValue)
        {
           // healthBar.enabled = true;
        }
        //hitpoints -= dmg;
        //healthSlider.value = hitpoints;

        if (hitpoints <= 0 && !isDead)
        {
            isDead = true;
            Invoke("EnemyDead", 0f);
        }
    }
    private void EnemyDead()
    {
        Debug.Log("Dead");
        stateMachine = GetComponent<StateMachine>();
        enemy = GetComponent<Enemy>();
        enemyAttackScript= GetComponent<EnemyAttackScript>();
       
        stateMachine.enabled= false;
        enemyAttackScript.enabled= false;
        enemy.enabled= false;
        
        
    }

    
}
