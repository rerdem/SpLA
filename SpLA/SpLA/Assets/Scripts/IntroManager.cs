using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
		//DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		GameObject textObject = new GameObject("textcrawl");
		textObject.transform.SetParent(canvas.transform);

		ContentSizeFitter fitterComponent = textObject.AddComponent<ContentSizeFitter>();
		fitterComponent.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

		textComponent = textObject.AddComponent<Text>();
		textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		textComponent.fontSize = 36;
		textComponent.color = Color.white;
		if (GameManager.gm.introPlayed) {
			textComponent.text = GameManager.gm.getOutroText();
		}
		else {
			textComponent.text = GameManager.gm.getIntroText();
		}
		//textComponent.text = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.   \n\nDuis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi. Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.   \n\nUt wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent luptatum zzril delenit augue duis dolore te feugait nulla facilisi.   \n\nNam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.   \n\nDuis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis.   \n\nAt vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, At accusam aliquyam diam diam dolore dolores duo eirmod eos erat, et nonumy sed tempor et et invidunt justo labore Stet clita ea et gubergren, kasd magna no rebum. sanctus sea sed takimata ut vero voluptua. est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur";

		textComponent.rectTransform.anchorMin = new Vector2(0f, 0f);
		textComponent.rectTransform.anchorMax = new Vector2(1f, 0f);
		textComponent.rectTransform.pivot = new Vector2(0.5f, 0f);
		//left - bottom
		textComponent.rectTransform.offsetMin = new Vector2(50f, textComponent.rectTransform.offsetMin.y);
		//right - top
		textComponent.rectTransform.offsetMax = new Vector2(-50f, textComponent.rectTransform.offsetMax.y);
		//rect size is only updated at end of fram, to get new value immediately:
		TextGenerator textGenerator = new TextGenerator();
		TextGenerationSettings generationSettings = textComponent.GetGenerationSettings(textComponent.rectTransform.rect.size);
		float offset = textGenerator.GetPreferredHeight(textComponent.text, generationSettings) - (textComponent.rectTransform.localPosition.y / 2);

		textComponent.rectTransform.localPosition = new Vector3(textComponent.rectTransform.localPosition.x, -offset);

		StartCoroutine(moveText(true));
	}

	IEnumerator moveText(bool moveInProgress) {
		if (moveInProgress) {
			for (float i = textComponent.rectTransform.localPosition.y; i < (Screen.height / 2); i += (Time.deltaTime * movementSpeed)) {
				textComponent.rectTransform.localPosition = new Vector3(textComponent.rectTransform.localPosition.x, i);
				yield return null;
			}
		}

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

	// Update is called once per frame
	void Update () {
		
	}
}
