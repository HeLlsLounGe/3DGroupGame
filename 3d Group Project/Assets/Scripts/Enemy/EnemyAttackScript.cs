using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public int warnings, enemyDamage;
    private int currentWarning;
    public float warningRate, counterWindow;
    private float warningRateTimer, counterWindowTimer, leniancy;
    [SerializeField] AudioSource warningSound;
    [SerializeField] AudioSource fire;
    public SwordScript swordScript;
    public CollectiveEnemyBrain brain;
    public GameObject brainObject;
    public GameObject player;
    public GameObject playerReal;
    bool bulletState = false;
    public bool firingState = false;

    LineBehavior lineBehavior;


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
            firingState = true;
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
    } 
    public void Countered()
    {
        lineBehavior.enabled = true;
        bulletState = false;
        GetComponent<EnemyHealthBar>().TakeDamage(enemyDamage);
        firingState = false;
        lineBehavior.DeflectedLineDraw();
        brain.CanFireAgain();
    }

    void Uncountered()
    {
        if (swordScript.leniancyTimer <= 0 && bulletState)
        {
            bulletState = false;
            playerReal.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
            lineBehavior.enabled = true;
            firingState = false;
            lineBehavior.UndeflectedLineDraw();
            brain.CanFireAgain();
        }
        else if (swordScript.leniancyTimer > 0)
        {
           Countered();
        }
        bulletState = false;
    }

    void Retry()
    {
        PreWindUp();
    }
}
