using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Switches the image of the object depending play status of music.
/// </summary>
public class MusicImageSwitcher : MonoBehaviour {

	public Sprite onSprite;
	public Sprite offSprite;

	void Update () {
		if (GameManager.gm.playMusic) {
			gameObject.GetComponent<Image>().sprite = onSprite;
		}
		else {
			gameObject.GetComponent<Image>().sprite = offSprite;
		}
	}
}
