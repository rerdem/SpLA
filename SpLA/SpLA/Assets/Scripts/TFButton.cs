using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles setup of a button for exercises dependent on one correct answer.
/// </summary>
public class TFButton : MonoBehaviour {

	public Button button;
	public Text buttonLabel;
	public bool correct = false;

	void Start () {
		button.onClick.AddListener(handleClick);
	}

	/// <summary>
	/// Setup the button.
	/// </summary>
	/// <param name="buttonstring">Answer displayed on the button.</param>
	/// <param name="tf">If set to <c>true</c> the answer is correct, if set to <c>false</c> it is not.</param>
	public void setup(string buttonstring, bool tf) {
		buttonLabel.text = buttonstring;
		correct = tf;
	}

	/// <summary>
	/// Handles actions triggered when the button is clicked.
	/// </summary>
	public void handleClick() {
		TownManager.instance.checkTF(correct);
	}
}
