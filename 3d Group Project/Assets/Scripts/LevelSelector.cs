using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] int nextLevel;
    [SerializeField] bool usedForFlag = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void Level5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void Level6()
    {
        SceneManager.LoadScene("Level6");
    }
    public void Level7()
    {
        SceneManager.LoadScene("Level7");
    }
    public void LoadMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            if (nextLevel == 1)
            {
                Invoke("Level1", 2);
            }
            else if (nextLevel == 2)
            {
                Invoke("Level2", 2);
            }
            else if (nextLevel == 3)
            {
                Invoke("Level3", 2);
            }
            else if (nextLevel == 4)
            {
                Invoke("Level4", 2);
            }
            else if (nextLevel == 5)
            {
                Invoke("Level5", 2);
            }
            else if (nextLevel == 6)
            {
                Invoke("Level6", 2);
            }
            else if (nextLevel == 7)
            {
                Invoke("Level7", 2);
            }
            
        }
    }
}
