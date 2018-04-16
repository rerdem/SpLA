using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activates a GameObject and deactivates it after display time is reached.
/// </summary>
public class ImageDeactivate : MonoBehaviour {

	public float displayTime = 5.0f;

	private float internalTimer;

	/// <summary>
	/// Activates this object and starts the timer.
	/// </summary>
	public void activate() {
		internalTimer = displayTime;
		gameObject.SetActive(true);
	}

	void Start () {
		internalTimer = displayTime;
	}
	
	void Update () {
		if (gameObject.activeInHierarchy) {
			internalTimer -= Time.deltaTime;
		}

		if (internalTimer < 0) {
			gameObject.SetActive(false);
		}
	}
}
