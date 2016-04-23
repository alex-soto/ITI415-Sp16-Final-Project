/*
AbstractBehavior script adapted from "Advanced Unity 2D: Platformer Player Movement"
Source URL: http://www.lynda.com/Unity-tutorials/Building-input-manager/367449/387290-4.htm
 */

using UnityEngine;
using System.Collections;

public abstract class AbstractBehavior : MonoBehaviour {

	public Buttons[] inputButtons;

	protected InputState inputState;
	protected Rigidbody2D rigidBody;

	protected virtual void Awake () {
		inputState = GetComponent<InputState> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
