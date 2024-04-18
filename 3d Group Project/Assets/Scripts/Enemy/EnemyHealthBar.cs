using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitpoints = 50;
    [SerializeField] private int switchPoint;
    private int MaxHP, switchTimer;
    //Slider healthSlider;
    public bool isDead = false;
    public int randomPoint;
    GameObject enemyBrain;
    CollectiveEnemyBrain brain;
    NavMeshAgent agent;
    Animator animator;
    StateMachine stateMachine;
    Enemy enemy;
    EnemyAttackScript enemyAttackScript;
    EnemyMovementTracker movementTracker;
    WeakPointManager[] weakPointManagers;
    BoxCollider[] boxCollider;
    //public Canvas healthBar;

    private void Awake()
    {
        enemyBrain = GameObject.FindWithTag("EnemyBrain");
        brain = enemyBrain.GetComponent<CollectiveEnemyBrain>();
        weakPointManagers = GetComponentsInChildren<WeakPointManager>();
        //healthSlider = GetComponentInChildren<Slider>();
        MaxHP = hitpoints;
        switchTimer = switchPoint;
        animator = GetComponentInChildren<Animator>();
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

        brain.CanFireAgain();

        stateMachine = GetComponent<StateMachine>();
        enemy = GetComponent<Enemy>();
        enemyAttackScript= GetComponent<EnemyAttackScript>();
        agent = GetComponent<NavMeshAgent>();
        movementTracker = GetComponent<EnemyMovementTracker>();
        boxCollider = GetComponents<BoxCollider>();
        stateMachine.enabled= false;
        enemyAttackScript.enabled= false;

        brain.CanFireAgain();

        enemy.enabled= false;
        agent.enabled= false;
        movementTracker.enabled = false;
        for (int i = 0; i < boxCollider.Length; i++)
        {
            boxCollider[i].enabled= false;
        }
        for (int i = 0; i < weakPointManagers.Length; i++)
        {
            weakPointManagers[i].KillFunction();
        }
        animator.SetTrigger("Fall");
        
        
    }

    
}
