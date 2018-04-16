using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds the data of an exercise.
/// </summary>
[System.Serializable]
public class DataQuestion {
	public string type;
	public string questionText;
	public DataAnswer[] answers;
}
