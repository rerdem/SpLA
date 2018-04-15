using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFade : MonoBehaviour {

	// the image you want to fade, assign in inspector
	public Image image;
	public float fadeTime = 1.0f;

	public void fadeImage(bool fadeAway) {
		StartCoroutine(FadeImage(true));
	}

	IEnumerator FadeImage(bool fadeAway) {
		// fade from opaque to transparent
		if (fadeAway) {
			// loop over 1 second backwards
			for (float i = fadeTime; i >= 0; i -= Time.deltaTime) {
				// set color with i as alpha
				image.color = new Color(1, 1, 1, i);
				yield return null;
			}
		}

		if (image.color.a < 3) {
			Destroy(gameObject);
		}
	}
}
