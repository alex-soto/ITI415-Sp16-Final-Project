using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private bool canJump = false;
	private Rigidbody2D rigidBody;
	public float colliderFriction = 0.5f;
	public float drag = 0.5f;
	public float angularDrag = 0.5f;

	void Awake(){

	}

	void FixedUpdate(){
		GetComponent<BoxCollider2D> ().sharedMaterial.friction = colliderFriction;
		GetComponent<Rigidbody2D> ().drag = drag;
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
