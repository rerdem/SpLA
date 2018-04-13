using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownManager : MonoBehaviour {

	public static TownManager instance = null;

	public TownGenerator generator;
	public GameObject player;
	public GameObject mcPanel;
	public GameObject mpPanel;
	public GameObject tfPanel;
	public Text mcQuestion;
	public Text mpQuestion;
	public Text tfQuestion;
	public GameObject[] mcButtons;
	public GameObject[] mpButtons;
	public GameObject[] tfButtons;
	public GameObject grammarVocabPanel;

	private GameObject[] npcs;
	private DataQuestion[] exercises;
	private int activeNPCs;
	private int currentNPCindex;
	private bool exitPlaced = false;

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
		//Debug.Log(npcs[0]);
		activeNPCs = npcs.Length;
		Instantiate(player, new Vector2 (7, 1), Quaternion.identity);
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
		deactivateNPC(currentNPCindex);
		//deactivate movement
		GameManager.gm.inExercise = true;
		//show cursor
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void checkTF(bool isCorrect) {
		
	}

	private int getNPCindex(GameObject askedNPC) {
		for (int i = 0; i < npcs.Length; i++) {
			if (GameObject.ReferenceEquals(askedNPC, npcs[i])) {
				return i;
			}
		}
		return 0;
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
			//trigger exit appearance message
		}
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
