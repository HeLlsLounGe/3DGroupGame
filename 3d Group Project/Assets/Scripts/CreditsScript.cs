using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] float Next = 45f;
    float timer = 0;
    [SerializeField] bool endcredits = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > Next)
        {
            if (endcredits)
            {
                Skip();
            }else
            {
                LoadLevel();
            }
            
        }
    }
    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
