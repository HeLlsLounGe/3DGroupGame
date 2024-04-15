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
    public GameObject player;
    public GameObject playerReal;
    bool bulletState = false;

    LineBehavior lineBehavior;


   void Awake()
    { 
        counterWindowTimer = counterWindow;
        player = GameObject.FindGameObjectWithTag("Sword");
        playerReal = GameObject.FindGameObjectWithTag("Player");
        currentWarning = warnings;
        swordScript =  player.GetComponent<SwordScript>();
        lineBehavior = GetComponentInChildren<LineBehavior>();
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

        if(Input.GetKeyDown("q"))
            WindUp(); 
    }

  public void WindUp()
    {
        if (currentWarning > 0)
        { Invoke(nameof(Sound),0f); }

        else if (currentWarning <= 0)
        { Invoke(nameof(Window),0f); }
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
        bulletState = false;
        GetComponent<EnemyHealthBar>().TakeDamage(enemyDamage);
        lineBehavior.DeflectedLineDraw();
    }

    void Uncountered()
    {
        if (swordScript.leniancyTimer <= 0 && bulletState)
        {
            bulletState = false;
            playerReal.GetComponent<PlayerHealth>().TakeDamage(enemyDamage);
            lineBehavior.UndeflectedLineDraw();
        }
        else if (swordScript.leniancyTimer > 0)
        {
           Countered();
        }
        bulletState = false;
    }
}
