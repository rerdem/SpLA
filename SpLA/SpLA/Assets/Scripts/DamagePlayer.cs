﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Player") {
			LevelManager.instance.respawnPlayer();
		}
	}

}