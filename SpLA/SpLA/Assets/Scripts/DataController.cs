using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataController : MonoBehaviour {

	public string teachinglanguage;
	public string learninglanguage;
	public string intro;
	public DataLecture[] allLectures;
	public string outro;

	//public DataGame gameData;

	private string gameDataFileName = "data.json";

	void Start() {
		DontDestroyOnLoad(gameObject);
		LoadGameData();
	}
		
//	public void SaveGameData() {
//		gameDataFileName = "data2.json";
//		string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
//
//		string contents = JsonUtility.ToJson(gameData, true);
//		File.WriteAllText(filePath, contents);
//	}

	private void LoadGameData() {
		string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

		if (File.Exists(filePath)) {
			string dataAsJson = File.ReadAllText(filePath);

			Debug.Log(dataAsJson);

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

	public DataLecture GetLectureData(int lectureID) {
		return allLectures [lectureID];
	}
}
