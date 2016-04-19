using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool canJump = false;
	private Rigidbody2D rigidBody;

	[Header("Inspector variables")]
	public float moveSpeed = 5f;
	public float jumpDistance = 20f;
	public int moveDirection;

	void Awake(){
		rigidBody = GetComponent<Rigidbody2D>();
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
//		Vector3 pos = transform.position;
//		pos.x += xAxis * moveSpeed * Time.deltaTime;
		if (canJump && Input.GetKeyDown("space")) {
			yAxis = jumpDistance;
			canJump = false;
		} 
//		transform.position = pos;
		rigidBody.velocity = new Vector2 (moveSpeed * xAxis, yAxis);
		//print ("xAxis: " + xAxis + " yAxis: " + yAxis);

	}


}
