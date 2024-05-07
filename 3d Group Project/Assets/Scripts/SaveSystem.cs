using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveSystem : MonoBehaviour
{
    private static string keyWord = "123456789";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
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
        SaveData myData = new SaveData();
        myData.x = transform.position.x;
        myData.y = transform.position.y;
        myData.z = transform.position.z;
        string myDataString = JsonUtility.ToJson(myData);
        Debug.Log(Application.persistentDataPath);
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        myDataString = EncryptDecryptData(myDataString);
        System.IO.File.WriteAllText(file, myDataString);
    }
    public void Load()
    {
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        if (File.Exists(file))
        {
            var jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            SaveData myData = JsonUtility.FromJson<SaveData>(jsonData);
            transform.position = new Vector3(myData.x, myData.y, myData.z);
        }
    }

    public string EncryptDecryptData(string data)
    {
        string result = "";
        for(int i = 0; i <data.Length; i++)
        {
            result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
        }
        return result;
    }
}
[System.Serializable]
public class SaveData
{
    public float x;
    public float y;
    public float z;
}
