using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public int warnings = 3;
    private int currentWarning;
    public float warningRate = 0.2f;
    private float warningRateTimer;
    [SerializeField] AudioSource warningSound;
    [SerializeField] AudioSource fire;


   void Start()
    {
        currentWarning = warnings;
    }

    void Update()
    {
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
       Invoke(nameof(WindUp), warningRate); 
    }

    void Attack()
    {
       fire.Play();
       currentWarning = warnings;
    } 
}
