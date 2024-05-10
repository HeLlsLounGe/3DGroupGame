using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOnLevelLoad : MonoBehaviour
{
    SavedData save;
    LevelSave saveFunction;
    private void Awake()
    {
        //save = GameObject.FindGameObjectWithTag("SaveData").GetComponent<SavedData>();
        saveFunction = GameObject.FindGameObjectWithTag("SaveData").GetComponent<LevelSave>();
        //saveFunction.Save();
    }
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("foo");
        saveFunction.Save();
    }
}
