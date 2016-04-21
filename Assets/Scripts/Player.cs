using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool canJump = false;
	private Rigidbody2D rigidBody;

	void Awake(){

	}

	void Update () {

	}

	// Allows player to jump when touching a surface
	void OnCollisionEnter2D(Collision2D other){
		canJump = true;
	}

	// If player stops touching a surface, they can no longer jump
	void OnCollisionExit2D(){
		canJump = false;
	}

}
