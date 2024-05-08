using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LevelSave : MonoBehaviour
{
    private static string keyWord = "123456789";
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Save();

        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Load();
        }
    }

    public void Save()
    {
        Scene myScene = SceneManager.GetActiveScene();
        SavedData myData = new SavedData();
        myData.currentScene = myScene.name;
        string myDataString = JsonUtility.ToJson(myData);
        Debug.Log(Application.persistentDataPath);
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        myDataString = EncryptDecryptData(myDataString);
        System.IO.File.WriteAllText(file, myDataString);
    }
    public void Load()
    {
        MainMenu mainMenu = GameObject.FindGameObjectWithTag("SaveData").GetComponent<MainMenu>();
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        if (File.Exists(file))
        {
            var jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            SavedData myData = JsonUtility.FromJson<SavedData>(jsonData);
            mainMenu.levelString = myData.currentScene;
        }
    }

    public string EncryptDecryptData(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
        }
        return result;
    }
}

[System.Serializable]
public class SavedData
{
    public int currentLevel;
    public string currentScene;
}

