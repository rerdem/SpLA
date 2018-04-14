using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour {

	public float maxSpeed = 10f;
	public float jumpForce = 700f;

	bool facingRight = true;

	private Animator animator;
	private Rigidbody2D rigid;
	private SpriteRenderer render;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();

		//cooldownTimer = interactCooldown + 0.1f;
	}
	
	// Update is called once per frame
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

	void flip() {
		facingRight = !facingRight;
		render.flipX = !render.flipX;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.GetComponent<Collider2D>().tag == "NPC") {
			TownManager.instance.triggerExercise(col.gameObject);
		}
	}

}
