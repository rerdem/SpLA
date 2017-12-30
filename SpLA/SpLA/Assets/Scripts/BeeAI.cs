using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAI : MonoBehaviour {

	public float maxSpeed = 2.5f;

	bool facingRight = false;

	private Rigidbody2D rigid;
	private SpriteRenderer render;

	bool grounded = false;
	int groundedCooldown = 0;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (groundedCooldown <= 0) {
			grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		}
		else {
			groundedCooldown--;
		}

		//horizontal movement
		if (facingRight) {
			rigid.velocity = new Vector2 (maxSpeed, rigid.velocity.y);
		}
		else {
			rigid.velocity = new Vector2 (-maxSpeed, rigid.velocity.y);
		}

		//flip animations
		if (!grounded) {
			flip();
			grounded = true;
			groundedCooldown = 5;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag != "Player") {
			flip();
		}
	}

	void flip() {
		facingRight = !facingRight;
		render.flipX = !render.flipX;
	}
}
