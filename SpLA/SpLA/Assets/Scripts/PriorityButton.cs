using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriorityButton : MonoBehaviour {

	public Button button;
	public Text buttonLabel;
	public int priority = 0;
	private bool clicked = false;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(handleClick);
	}

	public void setup(string buttonstring, int prio) {
		buttonLabel.text = buttonstring;
		priority = prio;
		clicked = false;
	}

	public void handleClick() {
		//trigger return priority;
		if (!clicked) {
			clicked = TownManager.instance.checkPrio(priority);
		}
	}
}
