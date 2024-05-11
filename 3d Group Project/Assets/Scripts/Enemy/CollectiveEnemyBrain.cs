using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiveEnemyBrain : MonoBehaviour
{
    public bool canFire = true;

    public void HasFired()
    {
        canFire = false;
    }

    public void CanFireAgain()
    {
        canFire= true;
    }
}
