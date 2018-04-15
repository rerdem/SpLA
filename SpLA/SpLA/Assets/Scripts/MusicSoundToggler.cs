using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundToggler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gm.playMusic) {
			gameObject.GetComponent<AudioSource>().mute = false;
		}
		else {
			gameObject.GetComponent<AudioSource>().mute = true;
		}
	}
}
