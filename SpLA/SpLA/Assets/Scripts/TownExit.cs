using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies what happens when the player walks into the town exit.
/// </summary>
public class TownExit : MonoBehaviour {

	/// <summary>
	/// Checks of the player has collided with the town exit and loads the next scene.
	/// </summary>
	/// <param name="col">Object the collision has occured with.</param>
	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Player") {
			GameManager.gm.loadLevel("level");
		}
	}
}
