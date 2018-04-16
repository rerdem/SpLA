using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles player movement and controls.
/// </summary>
public class PlayerController2D : MonoBehaviour {

	public Transform groundCheck;
	public LayerMask whatIsGround;

	public float maxSpeed = 10f;
	public float jumpForce = 700f;

	private Animator animator;
	private Rigidbody2D rigid;
	private SpriteRenderer render;

	private bool facingRight = true;
	private bool grounded = false;
	private float groundRadius = 0.2f;


	void Start () {
		animator = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
		//check if player is on ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
		animator.SetBool("playerGrounded", grounded);

		//horizontal movement
		if (!GameManager.gm.inExercise) {
			float move = Input.GetAxis("Horizontal");
			rigid.velocity = new Vector2(move * maxSpeed, rigid.velocity.y);

			//flip animations
			if ((move > 0) && (!facingRight)) {
				flip();
			}
			else if ((move < 0) && (facingRight)) {
				flip();
			}

		}

		//trigger walking animation
		if (rigid.velocity.x != 0) {
			animator.SetBool("playerWalking", true);
		}
		else {
			animator.SetBool("playerWalking", false);
		}
	}

	void Update() {
		if (!GameManager.gm.inExercise) {
			//jump
			if ((grounded) && ((Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.W)))) {
				animator.SetBool("playerGrounded", false);
				rigid.AddForce(new Vector2(0,jumpForce));
			}
			//duck
			if ((Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.S))) {
				animator.SetBool("playerDucking", true);
			}
			if ((Input.GetKeyUp(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.S))) {
				animator.SetBool("playerDucking", false);
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

	/// <summary>
	/// Checks if the player collided with an NPC.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter2D(Collider2D col) {
		if (col.GetComponent<Collider2D>().tag == "NPC") {
			TownManager.instance.triggerExercise(col.gameObject);
		}
	}

}
