using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles setup for language select buttons in main menu.
/// </summary>
public class SampleButton : MonoBehaviour {

	public Button button;
	public Text buttonLabel;

	void Start () {
		button.onClick.AddListener(handleClick);
	}

	/// <summary>
	/// Setup the button.
	/// </summary>
	/// <param name="filename">Filename to be displayed on the button.</param>
	public void setup(string filename) {
		//".json" file extension is removed before display
		buttonLabel.text = filename.Substring(0, filename.Length - 5);
	}

	/// <summary>
	/// Handles actions triggered when the button is clicked.
	/// </summary>
	public void handleClick() {
		GameManager.gm.initiateLoadingGameData(buttonLabel.text);
	}
}
