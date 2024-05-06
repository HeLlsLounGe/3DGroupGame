using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] float Next = 45f;
    float timer = 0;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer < Next)
        {
            Skip();
        }
    }
    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
