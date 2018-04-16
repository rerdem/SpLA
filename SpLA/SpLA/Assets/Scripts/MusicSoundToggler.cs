using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mutes/Unmutes the AudioSource depending on music flag on GameManager.
/// </summary>
public class MusicSoundToggler : MonoBehaviour {

	void Update () {
		if (GameManager.gm.playMusic) {
			gameObject.GetComponent<AudioSource>().mute = false;
		}
		else {
			gameObject.GetComponent<AudioSource>().mute = true;
		}
	}
}
