using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject levelSelect;
    private void Start()
    {
        levelSelect = GameObject.FindGameObjectWithTag("Level Select");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LevelSelect()
    {
        levelSelect.GetComponent<Canvas>().enabled = true;
    }
}
