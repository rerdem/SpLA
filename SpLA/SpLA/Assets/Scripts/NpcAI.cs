using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Specifies behavior of friendly NPCs.
/// </summary>
public class NpcAI : MonoBehaviour {
	
	public float stopTime = 0.5f;
	public float moveSpeed = 0.025f;
	public float minMoveDistance = 2.5f;
	public float maxMoveDistance = 3.5f;

	private Rigidbody2D mainBody;
	private SpriteRenderer render;

	private float move;
	private float leftmost;
	private float rightmost;
	private bool facingRight = true;
	private bool isStopped = false;
	private float timer;

	void Start () {
		mainBody = GetComponentInParent<Rigidbody2D>();
		render = GetComponentInParent<SpriteRenderer>();

		leftmost = mainBody.position.x;
		rightmost = leftmost + Random.Range(minMoveDistance, maxMoveDistance);
	}

	void FixedUpdate () {
		if (!GameManager.gm.inExercise) {
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
	}

	/// <summary>
	/// Flip this object.
	/// </summary>
	void flip() {
		facingRight = !facingRight;
		render.flipX = !render.flipX;
	}
}
