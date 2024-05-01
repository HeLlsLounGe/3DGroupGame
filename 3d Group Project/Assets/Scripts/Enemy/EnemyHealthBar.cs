using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitpoints = 50;
    [SerializeField] private int switchPoint;
    [SerializeField] AudioSource hurtSound;
    private int MaxHP, switchTimer;
    //Slider healthSlider;
    public bool isDead = false, projectileEnemy = false, meleeEnemy = false;
    public int randomPoint;
    GameObject enemyBrain;
    GameObject player;
    CollectiveEnemyBrain brain;
    PlayerHealth playerhealth;
    NavMeshAgent agent;
    Animator animator;
    StateMachine stateMachine;
    Enemy enemy;
    WinFunction winFunction;
    ProjectileStateMachine proStateMachine;
    ProjectileEnemy proEnemy;
    EnemyAttackScript enemyAttackScript;
    EnemyMovementTracker movementTracker;
    MeleeEnemyAI meleeAI;
    WeakPointManager[] weakPointManagers;
    BoxCollider[] boxCollider;
    //public Canvas healthBar;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerhealth = player.GetComponent<PlayerHealth>();
        enemyBrain = GameObject.FindWithTag("EnemyBrain");
        brain = enemyBrain.GetComponent<CollectiveEnemyBrain>();
        winFunction = enemyBrain.GetComponent<WinFunction>();
        winFunction.Enemies++;
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
            hurtSound.Play();
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
        playerhealth.RestoreHealth(1);
        Debug.Log("Dead");
        winFunction.Enemies--;
        winFunction.WinCheck();
        
        if (projectileEnemy == true)
        { 
            ProjectileEnemyDead();
            return;
        }
        if (meleeEnemy == true)
        {
            MeleeEnemyDead();
            return;
        }
        brain.CanFireAgain();

        stateMachine = GetComponent<StateMachine>();
        enemy = GetComponent<Enemy>();
        enemyAttackScript= GetComponent<EnemyAttackScript>();
        agent = GetComponent<NavMeshAgent>();
        movementTracker = GetComponent<EnemyMovementTracker>();
        boxCollider = GetComponents<BoxCollider>();
        Destroy(stateMachine);
        Destroy(enemyAttackScript);
        Destroy(enemy);
        Destroy(agent);
        if (brain.canFire == false)
        { brain.CanFireAgain(); }
        Destroy(movementTracker);
        for (int i = 0; i < boxCollider.Length; i++)
        {
            Destroy(boxCollider[i]);
        }
        for (int i = 0; i < weakPointManagers.Length; i++)
        {
            weakPointManagers[i].KillFunction();
        }
        animator.SetTrigger("Fall");
        
        
    }
    private void ProjectileEnemyDead()
    {
        proStateMachine = GetComponent<ProjectileStateMachine>();
        proEnemy = GetComponent<ProjectileEnemy>();
        agent = GetComponent<NavMeshAgent>();
        movementTracker = GetComponent<EnemyMovementTracker>();
        boxCollider = GetComponents<BoxCollider>();
        Destroy(proStateMachine);
        Destroy(proEnemy);
        Destroy(agent);
        Destroy(movementTracker);
        for (int i = 0; i < boxCollider.Length; i++)
        {
            Destroy(boxCollider[i]);
        }
        for (int i = 0; i < weakPointManagers.Length; i++)
        {
            weakPointManagers[i].KillFunction();
        }
        animator.SetTrigger("Fall");
    }
    private void MeleeEnemyDead()
    {
        meleeAI = GetComponent<MeleeEnemyAI>();
        agent = GetComponent<NavMeshAgent>();
        movementTracker = GetComponent<EnemyMovementTracker>();
        boxCollider = GetComponents<BoxCollider>();
        Destroy(meleeAI);
        Destroy(agent);
        Destroy(movementTracker);
        for (int i = 0; i < boxCollider.Length; i++)
        {
            Destroy(boxCollider[i]);
        }
        for (int i = 0; i < weakPointManagers.Length; i++)
        {
            weakPointManagers[i].KillFunction();
        }
        animator.SetTrigger("Fall");
    }

}
