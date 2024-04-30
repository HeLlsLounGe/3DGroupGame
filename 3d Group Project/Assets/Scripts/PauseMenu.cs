using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	void Start () {
        GetComponent<Canvas>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Time scale = " + Time.timeScale);
		if (Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 1) {
			Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.None;
			GetComponent<Canvas> ().enabled = true;


		}else if(Input.GetKeyDown (KeyCode.Escape) && Time.timeScale == 0){
			Resume();
		}

	}

	public void Resume(){
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
		GetComponent<Canvas> ().enabled = false;
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void MainMenu()
	{
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
	public void ReloadScene()
	{
		string currentSceneName = SceneManager.GetActiveScene().name;
		SceneManager.LoadScene(currentSceneName);
	}


}
