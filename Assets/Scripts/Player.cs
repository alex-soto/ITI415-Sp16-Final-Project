using UnityEngine;
using System.Collections;

//enum Directions {
//	Left,
//	Right
//}

public class Player : MonoBehaviour {

	public float moveSpeed;
	public float jumpMultiplier;
	public float radius = 5f;
	public LayerMask layerMask;
	public bool isStanding;

	private bool canJump = false;
	private Rigidbody2D rigidBody;
	private Vector2 gizmoPosition;
	private bool hasJumped = false;
	private float jumpDelay = 0.2f;
	private float timeJumped;

	void Awake(){
		moveSpeed = 15f;
		jumpMultiplier = 2f;
		Vector2 pos = transform.position;
		gizmoPosition = new Vector2 (pos.x, pos.y);
		rigidBody = GetComponent<Rigidbody2D>();
		StrokeManager strokeManager = GetComponent<StrokeManager>();
	}

	void FixedUpdate(){

		float cancelDraw = Input.GetAxisRaw ("Fire2");
		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		radius = transform.localScale.x / 4f;
		Vector2 bottomPos = new Vector2 (transform.position.x, transform.position.y - (transform.localScale.y / 2));
		isStanding = Physics2D.OverlapCircle (bottomPos, radius, layerMask);

		if (horizontal != 0) {
			Move (horizontal);
		}

		if (vertical > 0 && canJump) {
			Jump (vertical);
		}

		// Allows player to jump when standing on a solid surface
		if (isStanding) {
			canJump = true;
		}

		// string debugTest = "draw, cancelDraw, horizontal, vertical: " + draw + " " + cancelDraw + " " + horizontal + " " + vertical;
		// Debug.Log (debugTest);
	}

	void Move(float horizontal){
		Vector2 vel = rigidBody.velocity;
		rigidBody.velocity = new Vector2 (moveSpeed * horizontal, vel.y);
	}

	void Jump(float vertical){
		if (!hasJumped) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpMultiplier * moveSpeed);
			timeJumped = Time.time;
			hasJumped = true;
		} else if (Time.time >= jumpDelay + timeJumped) {
			hasJumped = false;
		} else {
			canJump = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		Debug.Log ("Collider: " + collider);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;

		Vector3 leftPos = new Vector3(transform.position.x - (transform.localScale.x / 2), 
		                          transform.position.y, 1);
		Vector3 rightPos = new Vector3(transform.position.x + (transform.localScale.x / 2), 
		                              transform.position.y, 1);
		Vector3 bottomPos = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2), 1);;
		// Vector3 pos = new Vector3(standingX, standingY, 1);
		Gizmos.DrawWireSphere (leftPos, radius);
		Gizmos.DrawWireSphere (rightPos, radius);
		Gizmos.DrawWireSphere (bottomPos, radius);
	}

}
