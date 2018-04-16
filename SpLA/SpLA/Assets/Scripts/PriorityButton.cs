using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles setup of a button for exercises dependent on button press order.
/// </summary>
public class PriorityButton : MonoBehaviour {

	public Button button;
	public Text buttonLabel;
	public int priority = 0;

	private bool clicked = false;

	void Start () {
		button.onClick.AddListener(handleClick);
	}

	/// <summary>
	/// Setup the button.
	/// </summary>
	/// <param name="buttonstring">Answer displayed on the button.</param>
	/// <param name="prio">Order of the button.</param>
	public void setup(string buttonstring, int prio) {
		buttonLabel.text = buttonstring;
		priority = prio;
		clicked = false;
	}

	/// <summary>
	/// Handles actions triggered when the button is clicked.
	/// </summary>
	public void handleClick() {
		if (!clicked) {
			clicked = TownManager.instance.checkPrio(priority);
		}
	}
}
