using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Fades out the attached Image object over the course of the specified fade time.
/// </summary>
public class ImageFade : MonoBehaviour {

	public Image image;
	public float fadeTime = 1.0f;

	/// <summary>
	/// Triggers the fading effect.
	/// </summary>
	/// <param name="fadeAway">If set to <c>true</c> fade away.</param>
	public void fadeImage(bool fadeAway) {
		StartCoroutine(FadeImage(true));
	}

	/// <summary>
	/// Fades the image.
	/// </summary>
	/// <returns>The image.</returns>
	/// <param name="fadeAway">If set to <c>true</c> fade away.</param>
	IEnumerator FadeImage(bool fadeAway) {
		if (fadeAway) {
			for (float i = fadeTime; i >= 0; i -= Time.deltaTime) {
				image.color = new Color(1, 1, 1, i);
				yield return null;
			}
		}

		//destroy this object after it has faded enough
		if (image.color.a < 3) {
			Destroy(gameObject);
		}
	}
}
