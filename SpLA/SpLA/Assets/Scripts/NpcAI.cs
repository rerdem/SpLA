using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAI : MonoBehaviour {

	private Rigidbody2D mainBody;
	private SpriteRenderer render;

	public float stopTime = 0.5f;
	public float moveSpeed = 0.025f;
	public float minMoveDistance = 2.5f;
	public float maxMoveDistance = 3.5f;
	private float move;

	private float leftmost;
	private float rightmost;

	private bool facingRight = true;
	private bool isStopped = false;

	private float timer;

	// Use this for initialization
	void Start () {
		mainBody = GetComponentInParent<Rigidbody2D>();
		render = GetComponentInParent<SpriteRenderer>();

		leftmost = mainBody.position.x;
		rightmost = leftmost + Random.Range(minMoveDistance, maxMoveDistance);
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (isStopped) {
			move = 0;

			timer += Time.deltaTime;
			if (timer >= stopTime) {
				isStopped = false;
				timer = 0;
			}
		}
		else {
			if (!facingRight) {
				move = moveSpeed;
			}
			else {
				move = -moveSpeed;
			}
		}

		mainBody.MovePosition(mainBody.position + Vector2.left * move);

		if (!facingRight) {
			if (mainBody.position.x < leftmost) {
				flip();
				isStopped = true;
			}
		}

		if (facingRight) {
			if (mainBody.position.x > rightmost) {
				flip();
				isStopped = true;
			}
		}
	}

	void flip() {
		facingRight = !facingRight;
		render.flipX = !render.flipX;
	}
}
