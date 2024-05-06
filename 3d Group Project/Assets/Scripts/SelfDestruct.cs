using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float Timer = 5f;
    void Awake()
    {
        Invoke("Destruct", Timer);
    }


   void Destruct ()
    {
        Destroy(gameObject);
    }
}
