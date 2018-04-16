using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the intro_outro scene. Controls creation of Text object, scrolling it across the screen and the transition to the next scene.
/// </summary>
public class IntroManager : MonoBehaviour {

	public static IntroManager instance = null;

	public GameObject canvas;
	public float movementSpeed = 50f;

	private Text textComponent;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy(gameObject);
	}

	void Start () {
		//create GameObject
		GameObject textObject = new GameObject("textcrawl");
		textObject.transform.SetParent(canvas.transform);

		//add and configure ContentSizeFitter
		ContentSizeFitter fitterComponent = textObject.AddComponent<ContentSizeFitter>();
		fitterComponent.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

		//add and configure Text
		textComponent = textObject.AddComponent<Text>();
		textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		textComponent.fontSize = 36;
		textComponent.color = Color.white;

		//load in the correct content
		if (GameManager.gm.introPlayed) {
			textComponent.text = GameManager.gm.getOutroText();
		}
		else {
			textComponent.text = GameManager.gm.getIntroText();
		}

		//position the Text outside the bottom side of the screen
		textComponent.rectTransform.anchorMin = new Vector2(0f, 0f);
		textComponent.rectTransform.anchorMax = new Vector2(1f, 0f);
		textComponent.rectTransform.pivot = new Vector2(0.5f, 0f);
		//left - bottom
		textComponent.rectTransform.offsetMin = new Vector2(50f, textComponent.rectTransform.offsetMin.y);
		//right - top
		textComponent.rectTransform.offsetMax = new Vector2(-50f, textComponent.rectTransform.offsetMax.y);
		//rect size is only updated at end of frame, to get new value immediately:
		TextGenerator textGenerator = new TextGenerator();
		TextGenerationSettings generationSettings = textComponent.GetGenerationSettings(textComponent.rectTransform.rect.size);
		float offset = textGenerator.GetPreferredHeight(textComponent.text, generationSettings) - (textComponent.rectTransform.localPosition.y / 2);
		textComponent.rectTransform.localPosition = new Vector3(textComponent.rectTransform.localPosition.x, -offset);

		//start the animation
		StartCoroutine(moveText(true));
	}

	/// <summary>
	/// Moves the text across the screen.
	/// </summary>
	/// <returns>The text.</returns>
	/// <param name="moveInProgress">If set to <c>true</c> text movement is in progress.</param>
	IEnumerator moveText(bool moveInProgress) {
		if (moveInProgress) {
			for (float i = textComponent.rectTransform.localPosition.y; i < (Screen.height / 2); i += (Time.deltaTime * movementSpeed)) {
				textComponent.rectTransform.localPosition = new Vector3(textComponent.rectTransform.localPosition.x, i);
				yield return null;
			}
		}

		//when the text leaves the top part of the screen, the next scene is loaded
		if ((textComponent.rectTransform.localPosition.y + (Screen.height / 2)) > (Screen.height - 10)) {
			if (!GameManager.gm.introPlayed) {
				GameManager.gm.introPlayed = true;
				GameManager.gm.loadLevel("town");
			}
			else {
				GameManager.gm.loadLevel("menu");
			}
		}
	}
}
