/*
FaceDirection script adapted from "Advanced Unity 2D: Platformer Player Movement"
Source URL: http://www.lynda.com/Unity-tutorials/Building-input-manager/367449/387290-4.htm
 */

using UnityEngine;
using System.Collections;

public class FaceDirection : AbstractBehavior {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool right = inputState.GetButtonValue (inputButtons [0]);
		bool left = inputState.GetButtonValue (inputButtons [1]);

		if (right) {
			inputState.direction = Directions.right;
		} else if (left) {
			inputState.direction = Directions.left;
		}

		transform.localScale = new Vector3 ((float)inputState.direction, 1, 1);
	}
}
