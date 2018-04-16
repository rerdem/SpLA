using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for holding an answer of an exercise.
/// </summary>
[System.Serializable]
public class DataAnswer {
	public string answerText;
	public bool isCorrect;
	public int queuePos;
}
