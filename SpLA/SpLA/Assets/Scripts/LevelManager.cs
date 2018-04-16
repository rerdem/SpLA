using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages gameplay in the platforming sections.
/// </summary>
public class LevelManager : MonoBehaviour {

	public static LevelManager instance = null;

	public GameObject player;
	public GameObject muteIcon;

	private GameObject playerObject;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy(gameObject);
	}

	void Start () {
		playerObject = Instantiate(player, new Vector2 (0, 4), Quaternion.identity);

		//hide cursor
		Cursor.visible = false;
	}
	
	void Update () {
		if ((GameManager.gm.playMusic) && (muteIcon.activeInHierarchy)) {
			muteIcon.SetActive(false);
		}
		if ((!GameManager.gm.playMusic) && (!muteIcon.activeInHierarchy)) {
			muteIcon.SetActive(true);
		}

		//skip level cheat button for evaluation purposes in context of this master thesis
		if (Input.GetKeyDown(KeyCode.C)) {
			winLevel();
		}
	}

	/// <summary>
	/// Loads the next scene.
	/// </summary>
	public void winLevel() {
		if (GameManager.gm.playOutro) {
			GameManager.gm.loadLevel("intro_outro");
		}
		else {
			GameManager.gm.loadLevel("town");
		}
	}

	/// <summary>
	/// Respawns the player.
	/// </summary>
	public void respawnPlayer() {
		playerObject.transform.position = new Vector2(0, 4);
	}
}
