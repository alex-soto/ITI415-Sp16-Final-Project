/*
SimpleMovement script adapted from "Advanced Unity 2D: Platformer Player Movement"
Source URL: http://www.lynda.com/Unity-tutorials/Building-input-manager/367449/387290-4.htm
 */

using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {

	public float speed = 50f;
	public float runMultiplier = 2f;
	public float jumpSpeed = 5f;
	public LayerMask collisionLayer;
	public bool isStanding;
	public Buttons[] input;
	public Color debugColor = Color.cyan;

	// Temporary inspector variables
	public float radius = 4f;

	private Rigidbody2D rigidBody;
	private InputState inputState;
	
	void Awake () {
		rigidBody = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		bool right = inputState.GetButtonValue (input [0]);
		bool left = inputState.GetButtonValue (input [1]);
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		Physics2D.OverlapCircle (pos, radius, collisionLayer);
		float tmpSpeed = speed;
		float velX;

		if (right || left) {

			velX = tmpSpeed * (float)inputState.direction;
		} else {
			velX = 0;
		}
		Debug.Log ("velX: " + velX + " rigidBody velocity: " + rigidBody.velocity);
		rigidBody.velocity = new Vector2 (velX, rigidBody.velocity.y);

	}

	void OnDrawGizmos(){
		Gizmos.color = debugColor;
		var pos = new Vector3 (transform.position.x, transform.position.y, 1);
		Gizmos.DrawWireSphere (pos, radius);
	}
}
