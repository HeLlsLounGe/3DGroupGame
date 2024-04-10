using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    public int warnings = 3;
    private int currentWarning;
    public float warningRate = 0.2f;
    private float warningRateTimer;
    [SerializeField] AudioClip warningSound;
    [SerializeField] AudioClip fire;


   void Start()
    {
        currentWarning = warnings;
    }

  public void WindUp()
    {
        if (currentWarning > 0)
        { Invoke(nameof(Sound)); }

        else if (currentWarning <= 0)
        { Invoke(nameof(Attack)); }
    }
  void Sound()
    {
        //warningSound.Play();
        currentWarning--;
       Invoke(nameof(WindUp), warningRate); 
    }

    void Attack()
    {
       // fire.Play();
        currentWarning = warnings;
    }
}
