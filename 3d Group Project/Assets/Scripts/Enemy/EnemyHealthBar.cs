using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitpoints = 50;
    [SerializeField] private int switchPoint;
    private int MaxHP, switchTimer;
    //Slider healthSlider;
    public bool isDead = false;
    public int randomPoint;
    StateMachine stateMachine;
    Enemy enemy;
    EnemyAttackScript enemyAttackScript;
    WeakPointManager[] weakPointManagers;
    //public Canvas healthBar;

    private void Awake()
    {
        weakPointManagers = GetComponentsInChildren<WeakPointManager>();
        //healthSlider = GetComponentInChildren<Slider>();
        MaxHP = hitpoints;
        switchTimer = switchPoint;
        //healthSlider.maxValue = MaxHP;
        //healthSlider.value = hitpoints;
        //healthBar.enabled = false;

        randomPoint = Random.Range(0, 6);
   
    }

    public void TakeDamage(int dmg)
    {
        hitpoints -= dmg;
        switchTimer -= dmg;
        if (switchTimer <= 0)
        { 
            randomPoint = Random.Range(0, 6);
            switchTimer = switchPoint;

            for (int i = 0; i < weakPointManagers.Length; i++)
            {
                weakPointManagers[i].WeakPointAssign();
            }
        }
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
