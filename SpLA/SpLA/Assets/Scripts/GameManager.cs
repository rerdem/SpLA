using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the general game loop and necessary flags.
/// </summary>
public class GameManager : MonoBehaviour {

	public static GameManager gm = null;

	public DataController dc;
	public bool tutorial = true;
	public bool inTutorial = false;
	public bool inExercise = false;
	public bool inGrammar = false;
	public bool inVocab = false;
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

		//set screen settings
		Screen.SetResolution(1024, 768, false);

		//set Cursor settings
		Cursor.visible = true;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.M)) {
			playMusic = !playMusic;
		}
	}

	/// <summary>
	/// Toggles the flag that determines, if music should be playing.
	/// </summary>
	public void toggleMusic() {
		playMusic = !playMusic;
	}

	/// <summary>
	/// Gets the introductory text.
	/// </summary>
	/// <returns>The introductory text.</returns>
	public string getIntroText() {
		return dc.intro;
	}

	/// <summary>
	/// Gets the ending text.
	/// </summary>
	/// <returns>The ending text.</returns>
	public string getOutroText() {
		return dc.outro;
	}

	/// <summary>
	/// Gets the title of the current lecture.
	/// </summary>
	/// <returns>The current lecture title.</returns>
	public string getLectureTitle() {
		return dc.allLectures[currentLecture].title;
	}

	/// <summary>
	/// Gets the grammar text of the current lecture.
	/// </summary>
	/// <returns>The current grammar text.</returns>
	public string getGrammarText() {
		return dc.allLectures[currentLecture].grammar;
	}

	/// <summary>
	/// Gets the vocabulary list of the current lecture.
	/// </summary>
	/// <returns>The current vocabulary list.</returns>
	public DataWord[] getVocabulary() {
		return dc.allLectures[currentLecture].vocabulary;
	}

	/// <summary>
	/// Gets the exercises of the current lecture.
	/// </summary>
	/// <returns>The current exercises.</returns>
	public DataQuestion[] getExercises() {		
		return dc.allLectures[currentLecture].exercises;
	}

	/// <summary>
	/// Initiates loading the game data and starts the game.
	/// </summary>
	/// <param name="filename">Filename game data is to be loaded from.</param>
	public void initiateLoadingGameData(string filename) {
		dc.loadGameData(filename + ".json");

		//skip intro scene, if there is no intro text
		if (dc.intro != "") {
			loadLevel("intro_outro");
		}
		else {
			loadLevel("town");
		}
	}

	/// <summary>
	/// Loads the next scene and advances current lecture if necessary.
	/// </summary>
	/// <param name="scenename">Name of the scene to be loaded.</param>
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
