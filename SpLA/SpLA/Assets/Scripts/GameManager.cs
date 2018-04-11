using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gm = null;

	public DataController dc;

	private int currentLecture = 0;

	void Awake() {
		if (gm == null)
			gm = this;
		else if (gm != null)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getNPCcount() {		
		return dc.allLectures[currentLecture].exercises.Length;
	}

	public DataQuestion[] getexercises() {		
		return dc.allLectures[currentLecture].exercises;
	}

	public void initiateLoadingGameData(string filename) {
		dc.loadGameData(filename + ".json");
		loadLevel("town");
	}

	public void loadLevel(string scenename) {
		if ((scenename == "town") && (SceneManager.GetActiveScene().name == "level")) {
			currentLecture++;
		}
		SceneManager.LoadScene(scenename);
	}
}
