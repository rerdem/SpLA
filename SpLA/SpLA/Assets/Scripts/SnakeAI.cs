using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAI : MonoBehaviour {

	public float moveSpeed = 0.025f;
	private float move;
	public float stopTime = 0.5f;
	public float moveDistance = 1f;

	private Rigidbody2D rigid;

	private bool goingUp = true;

	private float originalPosition;

	private float timer;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		originalPosition = rigid.position.y;
	}
	
	// Update is called once per frame
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
