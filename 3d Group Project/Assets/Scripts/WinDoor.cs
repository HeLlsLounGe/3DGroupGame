using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDoor : MonoBehaviour
{
    public bool activated = false;
    Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoorOpen()
    {
        animator.SetTrigger("Open");
        activated = true;
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && activated)
        {
            Debug.Log("DoorWorking");
        }
    }
}
