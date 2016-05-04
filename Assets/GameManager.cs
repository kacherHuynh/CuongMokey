using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public bool isGameOver =false ;
	public GameObject popupPrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isGameOver)
			ShowResult ();
	}

	public void ReloadGame(){ 
		isGameOver = false;
		Time.timeScale = 1;
		SceneManager.LoadScene ("Main");
	}

	void ShowResult(){
		Time.timeScale = 0;
		popupPrefabs.SetActive (true);
	}
}
