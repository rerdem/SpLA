using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies the behavior of any object with this script when touching the player.
/// </summary>
public class DamagePlayer : MonoBehaviour {

	/// <summary>
	/// Raises the CollisionEnter2D event and checks, if the colliding object is the player.
	/// </summary>
	/// <param name="col">GameObject the collision happened with.</param>
	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Player") {
			LevelManager.instance.respawnPlayer();
		}
	}
}
