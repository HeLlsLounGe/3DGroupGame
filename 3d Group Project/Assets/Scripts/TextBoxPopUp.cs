using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxPopUp : MonoBehaviour
{
    Canvas canvas;
    bool hasBeenTriggered;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        hasBeenTriggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hasBeenTriggered)
        {
            canvas.enabled = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            hasBeenTriggered = true;
        }

    }

    public void DoneReading()
    {
        Time.timeScale = 1;
        canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
