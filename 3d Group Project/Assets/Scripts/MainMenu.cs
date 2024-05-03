using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject levelSelect;
    Canvas levelSelectCanvas;
    private void Awake()
    {
        levelSelect = GameObject.FindGameObjectWithTag("Level Select");
        //levelSelectCanvas = levelSelect.GetComponent<Canvas>();
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
