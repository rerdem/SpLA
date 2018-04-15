using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour {

	public void toggle() {
		if (gameObject.activeInHierarchy) {
			gameObject.SetActive(false);
		}
		else {
			gameObject.SetActive(true);
		}
	}
}
