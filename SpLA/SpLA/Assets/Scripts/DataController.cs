using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// Handles loading of game data and allows acces to it.
/// </summary>
public class DataController : MonoBehaviour {

	public string teachinglanguage;
	public string learninglanguage;
	public string intro;
	public DataLecture[] allLectures;
	public string outro;

	void Start() {
		DontDestroyOnLoad(gameObject);
	}

	/// <summary>
	/// Loads the game data.
	/// </summary>
	/// <param name="gameDataFileName">Filename of the JSON file containing the game data.</param>
	public void loadGameData(string gameDataFileName) {
		string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

		if (File.Exists(filePath)) {
			string dataAsJson = File.ReadAllText(filePath);
			DataGame loadedData = JsonUtility.FromJson<DataGame>(dataAsJson);

			teachinglanguage = loadedData.teachinglanguage;
			learninglanguage = loadedData.learninglanguage;
			intro = loadedData.intro;
			allLectures = loadedData.allLectures;
			outro = loadedData.outro;
		}
		else {
			Debug.LogError("GameData not found!");
		}
	}

	/// <summary>
	/// Gets the all the data of the specified lecture.
	/// </summary>
	/// <returns>The DataLecture object.</returns>
	/// <param name="lectureID">ID of the lecture.</param>
	public DataLecture GetLectureData(int lectureID) {
		return allLectures[lectureID];
	}
}
