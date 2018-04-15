using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
		//DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		exercises = GameManager.gm.getExercises();
		generator.setup(exercises.Length);
		npcs = generator.getNpcs();
		activeNPCs = npcs.Length;
		Instantiate(player, new Vector2 (7, 1), Quaternion.identity);

		//show UI elements
		grammarButtonPrompt.SetActive(true);
		vocabButtonPrompt.SetActive(true);
		if (GameManager.gm.tutorial == true) {
			tutorialPanel.SetActive(true);
		}

		titleText.text = GameManager.gm.getLectureTitle();
		titlePanel.GetComponent<ImageDeactivate>().activate();

		//hide cursor
		Cursor.visible = false;
		//Cursor.lockState = CursorLockMode.Locked;
	}

	public void toggleGrammarPanel() {
		if (!grammarInitiated) {
			grammarText.text = GameManager.gm.getGrammarText();
			grammarInitiated = true;
		}

		if (!grammarPanel.activeInHierarchy) {
			grammarPanel.SetActive(true);

			GameManager.gm.inExercise = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else {
			grammarPanel.SetActive(false);
			GameManager.gm.inExercise = false;
			Cursor.visible = false;
		}
	}

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

		if (!vocabPanel.activeInHierarchy) {
			
			vocabPanel.SetActive(true);

			GameManager.gm.inExercise = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
		}
		else {
			vocabPanel.SetActive(false);
			GameManager.gm.inExercise = false;
			Cursor.visible = false;
		}
	}

	public void triggerExercise(GameObject askedNPC) {
		currentNPCindex = getNPCindex(askedNPC);
		//Debug.Log(currentNPCindex);
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
		//deactivate NPC
		//deactivateNPC(currentNPCindex);
		//deactivate movement
		GameManager.gm.inExercise = true;
		//show cursor
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void cancelExercise() {
		mcPanel.SetActive(false);
		mpPanel.SetActive(false);
		tfPanel.SetActive(false);
		//activate NPC
		//activateNPC(currentNPCindex);
		//deactivate movement
		GameManager.gm.inExercise = false;
		//show cursor
		Cursor.visible = false;
	}

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
			//Cursor.lockState = CursorLockMode.Locked;
		}
		else {
			falseIcon.GetComponent<ImageDeactivate>().activate();
		}
	}

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
			//Cursor.lockState = CursorLockMode.Locked;
		}

		return true;
	}

	private int getNPCindex(GameObject askedNPC) {
		for (int i = 0; i < npcs.Length; i++) {
			if (GameObject.ReferenceEquals(askedNPC, npcs[i])) {
				return i;
			}
		}
		return 0;
	}

	private void activateNPC(int index) {
		npcs[index].SetActive(true);
		activeNPCs++;
	}

	private void deactivateNPC(int index) {
		npcs[index].SetActive(false);
		activeNPCs--;
	}
	
	// Update is called once per frame
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

	private void fadeOutUI() {
		foreach (GameObject g in fadables) {
			if (g.activeInHierarchy) {
				g.GetComponent<ImageFade>().fadeImage(true);
			}
		}
		fadedUI = true;
		GameManager.gm.tutorial = false;
	}

	private void shuffleAnswers(DataAnswer[] answers) {
		for (int i = 0; i < answers.Length; i++) {
			DataAnswer temp = answers[i];
			int random = Random.Range(i, answers.Length);
			answers[i] = answers[random];
			answers[random] = temp;
		}
	}
}
