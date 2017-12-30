using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : MonoBehaviour {

	public GameObject leftEdge;
	public GameObject rightEdge;

	private Rigidbody2D mainBody;
	private SpriteRenderer render;

	public float stopTime = 0.5f;
	public float moveSpeed = 0.025f;
	private float move;

	private bool facingRight = false;
	private bool isStopped = false;

	private float timer;

	// Use this for initialization
	void Start () {
		mainBody = GetComponentInParent<Rigidbody2D>();
		render = GetComponentInParent<SpriteRenderer>();
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

		RaycastHit2D hitLeft = Physics2D.Raycast(leftEdge.transform.position, Vector2.down, 0.5f);
		RaycastHit2D hitRight = Physics2D.Raycast(rightEdge.transform.position, Vector2.down, 0.5f);

		RaycastHit2D hitWallLeft = Physics2D.Raycast(leftEdge.transform.position, Vector2.left, 0.01f);
		RaycastHit2D hitWallRight = Physics2D.Raycast(rightEdge.transform.position, Vector2.right, 0.01f);

		if (!facingRight) {
			if ((hitLeft.collider == null) || (hitWallLeft.collider != null)) {
				flip();
				isStopped = true;
			}
		}

		if (facingRight) {
			if ((hitRight.collider == null) || (hitWallRight.collider != null)) {
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
