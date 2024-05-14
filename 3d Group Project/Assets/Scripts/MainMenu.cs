using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelString;
    GameObject levelSelect;
    Canvas levelSelectCanvas;
    LevelSave save;

    private void Awake()
    {
        levelSelect = GameObject.FindGameObjectWithTag("Level Select");
        save = GameObject.FindGameObjectWithTag("SaveData").GetComponent<LevelSave>();
        //levelSelectCanvas = levelSelect.GetComponent<Canvas>();
    }
    public void StartGame()
    {
         SceneManager.LoadScene("Opening"); 
    }
    public void LoadGame()
    {
        string levelString = save.Load();
        SceneManager.LoadScene(levelString); 
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
