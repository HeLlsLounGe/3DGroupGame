using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public int warnings = 3;
    private int currentWarning;
    public float warningRate, counterWindow;
    private float warningRateTimer, counterWindowTimer, leniancy;
    [SerializeField] AudioSource warningSound;
    [SerializeField] AudioSource fire;
    public SwordScript swordScript;
    public GameObject player;


   void Awake()
    { 
        counterWindowTimer = counterWindow;
        player = GameObject.FindGameObjectWithTag("Sword");
        currentWarning = warnings;
        swordScript =  player.GetComponent<SwordScript>();
    }

    void Update()
    {  
        if (counterWindowTimer > 0)
        { counterWindowTimer -= Time.deltaTime; }

        if (currentWarning == 1)
        { leniancy = 0.1f; }

        else { leniancy = 0; }

        if(Input.GetButtonDown("Jump"))
            WindUp(); 
    }

  public void WindUp()
    {
        if (currentWarning > 0)
        { Invoke(nameof(Sound),0f); }

        else if (currentWarning <= 0)
        { Invoke(nameof(Attack),0f); }
    }
  void Sound()
    {
       warningSound.Play();
       currentWarning--;
       Invoke(nameof(WindUp), warningRate - leniancy); 
    }

    void Attack()
    {
       swordScript.DeflectState(); ;
       counterWindowTimer = counterWindow;
       currentWarning = warnings;
    } 
}
