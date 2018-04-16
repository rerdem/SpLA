using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles behavior of snakes.
/// </summary>
public class SnakeAI : MonoBehaviour {

	public float moveSpeed = 0.025f;
	public float stopTime = 0.5f;
	public float moveDistance = 1f;

	private Rigidbody2D rigid;

	private float move;
	private bool goingUp = true;
	private float originalPosition;
	private float timer;

	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		originalPosition = rigid.position.y;
	}
	
	void FixedUpdate () {
		if (goingUp) {
			move = moveSpeed;
		}
		else {
			move = -moveSpeed;
		}

		rigid.MovePosition(rigid.position + Vector2.up * move);

		if (rigid.position.y >= (originalPosition + moveDistance)) {
			goingUp = false;
		}

		if (rigid.position.y <= originalPosition) {
			goingUp = true;
		}
	}
}
