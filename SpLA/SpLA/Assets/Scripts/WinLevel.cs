using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies behavior when player enters level exit and laods next scene.
/// </summary>
public class WinLevel : MonoBehaviour {

	/// <summary>
	/// Checks if the player collided with level exit.
	/// </summary>
	/// <param name="col">Object the collision happened with.</param>
	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Player") {
			LevelManager.instance.winLevel();
		}
	}
}
