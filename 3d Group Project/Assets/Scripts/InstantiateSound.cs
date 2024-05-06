using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSound : MonoBehaviour
{
    [SerializeField] GameObject sound;
    void Awake()
    {
        GameObject Sounder = Instantiate(sound, transform.position, Quaternion.identity);
    }

   
    
}
