/*
SimpleMovement script adapted from "Advanced Unity 2D: Platformer Player Movement"
Source URL: http://www.lynda.com/Unity-tutorials/Building-input-manager/367449/387290-4.htm
 */

using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {

	public float speed = 2f;
	public Buttons[] input;

	private Rigidbody2D rigidBody;
	private InputState inputState;

	// Use this for initialization
	void Awake () {
		rigidBody = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
	}
	
	// Update is called once per frame
	void Update () {

		var right = inputState.GetButtonValue (input [0]);
		var left = inputState.GetButtonValue (input [1]);
		var velX = speed;

		if (right || left) {
			velX *= left ? -1 : 1;
		} else {
			velX = 0;
		}

		rigidBody.velocity = new Vector2 (velX, rigidBody.velocity.y);

	}
}
