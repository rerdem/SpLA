using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour {

	public Button button;
	public Text buttonLabel;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(handleClick);
	}

	public void setup(string filename) {
		buttonLabel.text = filename.Substring(0, filename.Length - 5);;
	}

	public void handleClick() {
		GameManager.gm.initiateLoadingGameData(buttonLabel.text);
	}
}
