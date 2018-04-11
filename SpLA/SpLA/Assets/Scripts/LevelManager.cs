using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance = null;

	public GameObject player;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy(gameObject);
		//DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		Instantiate(player, new Vector2 (0, 4), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void winLevel() {
		//GameManager.gm.loadLevel("town");
		Debug.Log("Level won!");
	}

	public void respawnPlayer() {
		destroyAllObjectsWithTag("Player");
		Instantiate(player, new Vector2 (0, 4), Quaternion.identity);
	}

	void destroyAllObjectsWithTag(string tag) {
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

		for (int i = 0; i < gameObjects.Length; i++) {
			Destroy(gameObjects[i]);
		}
	}
}
