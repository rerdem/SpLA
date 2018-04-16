using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds the entire game data.
/// </summary>
[System.Serializable]
public class DataGame {
	public string teachinglanguage;
	public string learninglanguage;
	public string intro;
	public DataLecture[] allLectures;
	public string outro;
}
