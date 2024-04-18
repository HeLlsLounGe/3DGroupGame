using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiveEnemyBrain : MonoBehaviour
{
    public bool canFire = true;


    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HasFired()
    {
        canFire = false;
    }

    public void CanFireAgain()
    {
        canFire = true;
        Debug.Log("Can Fire");
    }
}
