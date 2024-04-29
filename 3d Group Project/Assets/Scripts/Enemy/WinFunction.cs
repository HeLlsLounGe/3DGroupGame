using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinFunction : MonoBehaviour
{
    public int Spawners;
    public int Enemies;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinCheck()
    {
        if(Spawners == 0 && Enemies == 0)
        {
            Debug.Log("You're are winner");
        }
    }
}
