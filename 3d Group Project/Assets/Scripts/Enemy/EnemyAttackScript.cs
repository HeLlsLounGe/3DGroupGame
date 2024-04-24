using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public int warnings, enemyDamage;
    private int currentWarning, burstnumber;
    public float warningRate, counterWindow;
    private float warningRateTimer, counterWindowTimer, leniancy;
    [SerializeField] AudioSource warningSound;
    [SerializeField] AudioSource fire;
    public SwordScript swordScript;
    public CollectiveEnemyBrain brain;
    public GameObject brainObject;
    public GameObject player;
    public GameObject playerReal;
    public bool bulletState = false;

    LineBehavior lineBehavior;

    // MultiShot values
    [SerializeField] int additionalShots;
    int aSCounter;
    [SerializeField]
    float fireRate = 0.07f;


   void Awake()
    { 
        counterWindowTimer = counterWindow;
        player = GameObject.FindGameObjectWithTag("Sword");
        playerReal = GameObject.FindGameObjectWithTag("Player");
        brainObject = GameObject.FindGameObjectWithTag("EnemyBrain");
        currentWarning = warnings;
        swordScript =  player.GetComponent<SwordScript>();
        brain = brainObject.GetComponent<CollectiveEnemyBrain>();
        lineBehavior = GetComponentInChildren<LineBehavior>();
        lineBehavior.enabled = false;
        aSCounter = additionalShots;
    }

    void Update()
    {  
        if (counterWindowTimer > 0)
        { counterWindowTimer -= Time.deltaTime; }

        else if (counterWindowTimer <= 0 && bulletState)
        { Uncountered(); }

        if (currentWarning == 1)
        { leniancy = 0.1f; }

        else { leniancy = 0; }
    }

    public void PreWindUp()
    {
        if (brain.canFire)
        {
            WindUp();
        }

        else
        {
            Invoke("Retry", 2.0f);
        }
    }

  public void WindUp()
    {
            brain.HasFired();
            if (currentWarning > 0)
            { Invoke(nameof(Sound), 0f); }

            else if (currentWarning <= 0)
            { Invoke(nameof(Window), 0f); }   
    }
  void Sound()
    {
       warningSound.Play();
       currentWarning--;
       Invoke(nameof(WindUp), warningRate - leniancy); 
    }

    void Window()
    {
       player.GetComponent<SwordScript>().target = gameObject;
       swordScript.DeflectState(); 
       counterWindowTimer = counterWindow;
       currentWarning = warnings;
       bulletState = true;
       aSCounter = additionalShots;
    } 
    public void Countered()
    {
        lineBehavior.enabled = true;
        bulletState = false;
        GetComponent<EnemyHealthBar>().TakeDamage(enemyDamage);
        lineBehavior.DeflectedLineDraw();
        brain.CanFireAgain();
        fire.Play();

        if (aSCounter > 0)
        { 
            aSCounter--;
            Burst();
        }
    }

    void Uncountered()
    {
        if (swordScript.leniancyTimer <= 0 && bulletState)
        {      
            playerReal.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);

            lineBehavior.enabled = true;
            lineBehavior.UndeflectedLineDraw();

            brain.CanFireAgain();
            fire.Play();
        }
        else if (swordScript.leniancyTimer > 0)
        {
           Countered();
        }


        if (aSCounter > 0)
        {
            aSCounter--;
            Burst();
        }
        else { bulletState = false; }
    }

    void Burst()
    {
        Invoke("Uncountered", fireRate);
    }

    void Retry()
    {
        PreWindUp();
    }
}
