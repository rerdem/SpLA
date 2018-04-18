using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles gameplay in towns.
/// </summary>
public class TownManager : MonoBehaviour {

	public static TownManager instance = null;

	public TownGenerator generator;
	public GameObject player;

	public GameObject[] fadables;

	[Header("Title")]
	public GameObject titlePanel;
	public Text titleText;

	[Header("MC")]
	public GameObject mcPanel;
	public Text mcQuestion;
	public GameObject[] mcButtons;

	[Header("MP")]
	public GameObject mpPanel;
	public Text mpQuestion;
	public GameObject[] mpButtons;

	[Header("TF")]
	public GameObject tfPanel;
	public Text tfQuestion;
	public GameObject[] tfButtons;

	[Header("Grammar")]
	public GameObject grammarPanel;
	public Text grammarText;

	[Header("Vocabulary")]
	public GameObject vocabPanel;
	public Transform vocabContentPanel;
	public GameObject vocabTextPrefab;

	[Header("UI Icons")]
	public GameObject tutorialPanel;
	public GameObject musicButtonPrompt;
	public GameObject grammarButtonPrompt;
	public GameObject vocabButtonPrompt;
	public GameObject trueIcon;
	public GameObject falseIcon;
	public GameObject exitArrow;

	private GameObject[] npcs;
	private DataQuestion[] exercises;
	private int activeNPCs;
	private int currentNPCindex;
	private int currentAnswerPriority;
	private bool exitPlaced = false;
	private bool fadedUI = false;
	private bool grammarInitiated = false;
	private bool vocabularyInitiated = false;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy(gameObject);
	}

	void Start () {
		exercises = GameManager.gm.getExercises();
		generator.setup(exercises.Length);
		npcs = generator.getNpcs();
		activeNPCs = npcs.Length;
		Instantiate(player, new Vector2 (7, 1), Quaternion.identity);

		//show UI elements
		musicButtonPrompt.SetActive(true);
		grammarButtonPrompt.SetActive(true);
		vocabButtonPrompt.SetActive(true);
		if (GameManager.gm.tutorial == true) {
			tutorialPanel.SetActive(true);
			GameManager.gm.inTutorial = true;
		}

		//show title card
		titleText.text = GameManager.gm.getLectureTitle();
		titlePanel.GetComponent<ImageDeactivate>().activate();

		//hide cursor
		Cursor.visible = false;
	}

	/// <summary>
	/// Toggles the grammar UI panel.
	/// </summary>
	public void toggleGrammarPanel() {
		if (!grammarInitiated) {
			grammarText.text = GameManager.gm.getGrammarText();
			grammarInitiated = true;
		}

		if ((!GameManager.gm.inExercise) && (!GameManager.gm.inVocab) && (!GameManager.gm.inTutorial)) {
			if (!grammarPanel.activeInHierarchy) {
				grammarPanel.SetActive(true);
				GameManager.gm.inGrammar = true;
				Cursor.visible = true;
			}
			else {
				grammarPanel.SetActive(false);
				GameManager.gm.inGrammar = false;
				Cursor.visible = false;
			}
		}
	}

	/// <summary>
	/// Toggles the vocabulary UI panel.
	/// </summary>
	public void toggleVocabPanel() {
		if (!vocabularyInitiated) {
			DataWord[] vocabulary = GameManager.gm.getVocabulary();
			foreach (DataWord word in vocabulary) {
				GameObject newWord = GameObject.Instantiate(vocabTextPrefab);
				newWord.transform.SetParent(vocabContentPanel);
				newWord.GetComponent<Text>().text = word.word;

				newWord = GameObject.Instantiate(vocabTextPrefab);
				newWord.transform.SetParent(vocabContentPanel);
				newWord.GetComponent<Text>().text = word.translation;
			}
			vocabularyInitiated = true;
		}

		if ((!GameManager.gm.inExercise) && (!GameManager.gm.inGrammar) && (!GameManager.gm.inTutorial)) {
			if (!vocabPanel.activeInHierarchy) {
				vocabPanel.SetActive(true);
				GameManager.gm.inVocab = true;
				Cursor.visible = true;
			}
			else {
				vocabPanel.SetActive(false);
				GameManager.gm.inVocab = false;
				Cursor.visible = false;
			}
		}
	}

	/// <summary>
	/// Opens the exercise panel and sets it up according to exercise type.
	/// </summary>
	/// <param name="askedNPC">The NPC's GameObject the player collided with.</param>
	public void triggerExercise(GameObject askedNPC) {
		if ((!GameManager.gm.inGrammar) && (!GameManager.gm.inVocab) && (!GameManager.gm.inTutorial)) {
			currentNPCindex = getNPCindex(askedNPC);

			switch (exercises[currentNPCindex].type) {
				case "MC":
					mcQuestion.text = exercises[currentNPCindex].questionText;
					shuffleAnswers(exercises[currentNPCindex].answers);
					for (int i = 0; i < mcButtons.Length; i++) {
						mcButtons[i].GetComponent<TFButton>().setup(exercises[currentNPCindex].answers[i].answerText, exercises[currentNPCindex].answers[i].isCorrect);
					}
					mcPanel.SetActive(true);
					break;
				case "MP":
					mpQuestion.text = exercises[currentNPCindex].questionText;
					shuffleAnswers(exercises[currentNPCindex].answers);
					for (int i = 0; i < mpButtons.Length; i++) {
						mpButtons[i].GetComponent<PriorityButton>().setup(exercises[currentNPCindex].answers[i].answerText, exercises[currentNPCindex].answers[i].queuePos);
					}
					mpPanel.SetActive(true);
					currentAnswerPriority = 0;
					break;
				case "TF":
					tfQuestion.text = exercises[currentNPCindex].questionText;
					for (int i = 0; i < tfButtons.Length; i++) {
						tfButtons[i].GetComponent<TFButton>().setup(exercises[currentNPCindex].answers[i].answerText, exercises[currentNPCindex].answers[i].isCorrect);
					}
					tfPanel.SetActive(true);
					break;
				default:
					break;
			}

			//deactivate movement
			GameManager.gm.inExercise = true;

			//show cursor
			Cursor.visible = true;
		}
	}

	/// <summary>
	/// Cancels the exercise.
	/// </summary>
	public void cancelExercise() {
		mcPanel.SetActive(false);
		mpPanel.SetActive(false);
		tfPanel.SetActive(false);

		//deactivate movement
		GameManager.gm.inExercise = false;

		//show cursor
		Cursor.visible = false;
	}

	/// <summary>
	/// Checks if the clicked button is correct.
	/// </summary>
	/// <param name="isCorrect">If set to <c>true</c> it is correct, if <c>false</c> it is not.</param>
	public void checkTF(bool isCorrect) {
		if (isCorrect) {
			mcPanel.SetActive(false);
			tfPanel.SetActive(false);
			trueIcon.GetComponent<ImageDeactivate>().activate();

			//deactivate NPC
			deactivateNPC(currentNPCindex);

			//activate movement
			GameManager.gm.inExercise = false;

			//hide cursor
			Cursor.visible = false;
		}
		else {
			falseIcon.GetComponent<ImageDeactivate>().activate();
		}
	}

	/// <summary>
	/// Checks if the clicked button is correct.
	/// </summary>
	/// <returns><c>true</c>, if the clicked button has the correct priority, <c>false</c> otherwise.</returns>
	/// <param name="buttonPriority">Button priority.</param>
	public bool checkPrio(int buttonPriority) {
		if (buttonPriority == (currentAnswerPriority + 1)) {
			trueIcon.GetComponent<ImageDeactivate>().activate();
			currentAnswerPriority++;
		}
		else {
			falseIcon.GetComponent<ImageDeactivate>().activate();
			return false;
		}

		if (buttonPriority == 4) {
			mpPanel.SetActive(false);

			//deactivate NPC
			deactivateNPC(currentNPCindex);

			//activate movement
			GameManager.gm.inExercise = false;

			//hide cursor
			Cursor.visible = false;
		}

		return true;
	}

	/// <summary>
	/// Gets the NPC's index.
	/// </summary>
	/// <returns>The NPC's index.</returns>
	/// <param name="askedNPC">The NPC's GameObject the player collided with.</param>
	private int getNPCindex(GameObject askedNPC) {
		for (int i = 0; i < npcs.Length; i++) {
			if (GameObject.ReferenceEquals(askedNPC, npcs[i])) {
				return i;
			}
		}
		return 0;
	}

	/// <summary>
	/// Activates the NPC.
	/// </summary>
	/// <param name="index">Index of the NPC to be activated.</param>
	private void activateNPC(int index) {
		npcs[index].SetActive(true);
		activeNPCs++;
	}

	/// <summary>
	/// Deactivates the NPC.
	/// </summary>
	/// <param name="index">Index of the NPC to be deactivated.</param>
	private void deactivateNPC(int index) {
		npcs[index].SetActive(false);
		activeNPCs--;
	}
	
	void Update () {
		if ((!exitPlaced) && (activeNPCs == 0) && (!GameManager.gm.inExercise)) {
			generator.placeExit();
			exitPlaced = true;
			exitArrow.SetActive(true);
		}

		if ((!fadedUI) && (Input.GetAxis("Horizontal") != 0)) {
			fadeOutUI();
		}

		if (Input.GetKeyDown(KeyCode.G)) {
			toggleGrammarPanel();
		}

		if (Input.GetKeyDown(KeyCode.V)) {
			toggleVocabPanel();
		}
	}

	/// <summary>
	/// Fades out all fadable UI elements.
	/// </summary>
	private void fadeOutUI() {
		foreach (GameObject g in fadables) {
			if (g.activeInHierarchy) {
				g.GetComponent<ImageFade>().fadeImage(true);
			}
		}
		fadedUI = true;
		GameManager.gm.tutorial = false;
		GameManager.gm.inTutorial = false;
	}

	/// <summary>
	/// Shuffles the answers of an exercise.
	/// </summary>
	/// <param name="answers">Answer array to be shuffled.</param>
	private void shuffleAnswers(DataAnswer[] answers) {
		for (int i = 0; i < answers.Length; i++) {
			DataAnswer temp = answers[i];
			int random = Random.Range(i, answers.Length);
			answers[i] = answers[random];
			answers[random] = temp;
		}
	}
}
