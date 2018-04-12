using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour {

	public static TownManager instance = null;

	public TownGenerator generator;
	public GameObject player;
	public GameObject mcmpPanel;
	public GameObject tfPanel;
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
		activeNPCs = npcs.Length;
		Instantiate(player, new Vector2 (7, 1), Quaternion.identity);
	}

	public void triggerExercise(GameObject askedNPC) {
		currentNPCindex = getNPCindex(askedNPC);
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
		if ((!exitPlaced) && (activeNPCs == 0)) {
			generator.placeExit();
			exitPlaced = true;
			//trigger exit appearance message
		}
	}
}
