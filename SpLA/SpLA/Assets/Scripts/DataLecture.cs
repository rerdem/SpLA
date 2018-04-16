using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds the entire data of a single lecture.
/// </summary>
[System.Serializable]
public class DataLecture {
	public string title;
	public string grammar;
	public DataWord[] vocabulary;
	public DataQuestion[] exercises;
}
