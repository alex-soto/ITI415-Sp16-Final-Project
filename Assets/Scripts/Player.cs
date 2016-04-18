using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool canJump = false;
	private BoxCollider2D playerCollider;

	[Header("Inspector variables")]
	public float moveSpeed = 2;
	public float jumpDistance = 25;
	public int moveDirection;

	void Awake(){
		playerCollider = GetComponent<BoxCollider2D>();
	}

	void Update () {
		Move ();

	}

	// Allows player to jump when touching a surface
	void OnCollisionEnter2D(Collision2D other){
		canJump = true;
	}

	// If player stops touching a surface, they can no longer jump
	void OnCollisionExit2D(){
		canJump = false;
	}

	void Move(){
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis("Vertical");
		Vector3 pos = transform.position;

		pos.x += xAxis * moveSpeed * Time.deltaTime;
		if (canJump && Input.GetKeyDown("space")) {
			pos.y += jumpDistance * Time.deltaTime;
			canJump = false;
		} 
		transform.position = pos;

		print ("xAxis: " + xAxis + " yAxis: " + yAxis);

	}


}
