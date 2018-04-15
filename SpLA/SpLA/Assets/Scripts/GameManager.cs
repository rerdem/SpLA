using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gm = null;

	public DataController dc;
	public bool inExercise = false;
	public bool tutorial = true;
	public bool introPlayed = false;
	public bool playOutro = false;
	public bool playMusic = true;

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
		if (Input.GetKeyDown(KeyCode.M)) {
			playMusic = !playMusic;
		}
	}

	public void toggleMusic() {
		playMusic = !playMusic;
	}

	public string getIntroText() {
		return dc.intro;
	}

	public string getOutroText() {
		return dc.outro;
	}

	public string getLectureTitle() {
		return dc.allLectures[currentLecture].title;
	}

	public string getGrammarText() {
		return dc.allLectures[currentLecture].grammar;
	}

	public DataWord[] getVocabulary() {
		return dc.allLectures[currentLecture].vocabulary;
	}

	public DataQuestion[] getExercises() {		
		return dc.allLectures[currentLecture].exercises;
	}

	public void initiateLoadingGameData(string filename) {
		dc.loadGameData(filename + ".json");
		if (dc.intro != "") {
			loadLevel("intro_outro");
		}
		else {
			loadLevel("town");
		}
	}

	public void loadLevel(string scenename) {
		if ((scenename == "town") && (SceneManager.GetActiveScene().name == "level")) {
			currentLecture++;
		}

		if ((scenename == "level") && (currentLecture == (dc.allLectures.Length - 1))) {
			playOutro = true;
		}
		SceneManager.LoadScene(scenename);
	}
}
