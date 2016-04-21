/*
InputState script adapted from "Advanced Unity 2D: Platformer Player Movement"
Source URL: http://www.lynda.com/Unity-tutorials/Building-input-manager/367449/387290-4.htm
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonState {
	public bool value;
	public float holdTime = 0;
}

public enum Directions {
	right = 1,
	left = -1
}

public class InputState : MonoBehaviour {
	private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState> ();

	public Directions direction = Directions.right;
	public void SetButtonValue(Buttons key, bool value) {
		if (!buttonStates.ContainsKey (key)) 
			buttonStates.Add (key, new ButtonState ());

		var state = buttonStates [key];
		if (state.value && !value) {
			state.holdTime = 0;
		} else if (state.value && value) {
			state.holdTime += Time.deltaTime;
		}
		state.value = value;
	}

	public bool GetButtonValue(Buttons key){
		if (buttonStates.ContainsKey (key)) {
			return buttonStates [key].value;
		} else {
			return false;
		}
	}
}
