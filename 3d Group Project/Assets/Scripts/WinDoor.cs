using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDoor : MonoBehaviour
{
    public bool activated = false;
    AudioSource doorSound;
    Animator animator;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        doorSound = GetComponent<AudioSource>();
    }
    public void DoorOpen()
    {
        animator.SetTrigger("Open");
        activated = true;
        doorSound.Play();
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && activated)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }
}
