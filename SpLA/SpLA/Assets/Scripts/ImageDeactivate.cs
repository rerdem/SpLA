using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageDeactivate : MonoBehaviour {

	public float displayTime = 5.0f;

	private float internalTimer;

	public void activate() {
		internalTimer = displayTime;
		gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () {
		internalTimer = displayTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeInHierarchy) {
			internalTimer -= Time.deltaTime;
		}

		if (internalTimer < 0) {
			gameObject.SetActive(false);
		}
	}
}
