using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TFButton : MonoBehaviour {

	public Button button;
	public Text buttonLabel;
	public bool correct = false;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(handleClick);
	}

	public void setup(string buttonstring, bool tf) {
		buttonLabel.text = buttonstring;
		correct = tf;
	}

	public void handleClick() {
		//trigger return correct;
	}
}
