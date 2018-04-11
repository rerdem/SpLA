using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour {

	public static TownManager instance = null;

	public TownGenerator generator;

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
		generator.setup(GameManager.gm.getNPCcount());
		Instantiate(player, new Vector2 (7, 1), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
