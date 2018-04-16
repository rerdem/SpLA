using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toggles object.
/// </summary>
public class ToggleObject : MonoBehaviour {

	/// <summary>
	/// Toggle this object.
	/// </summary>
	public void toggle() {
		if (gameObject.activeInHierarchy) {
			gameObject.SetActive(false);
		}
		else {
			gameObject.SetActive(true);
		}
	}
}
