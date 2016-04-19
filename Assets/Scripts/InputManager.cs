/*
InputManager script taken from "Advanced Unity 2D: Platformer Player Movement"
Source URL: http://www.lynda.com/Unity-tutorials/Building-input-manager/367449/387290-4.htm
 */

using UnityEngine;
using System.Collections;

public enum Buttons {
	Right, 
	Left

}

public enum Condition{
	GreaterThan,
	LessThan
}

[System.Serializable]
public class InputAxisState{
	public string axisName;
	public float offValue;
	public Buttons button;
	public Condition condition;

	public bool value {
		get {
			var val = Input.GetAxis(axisName);

			switch(condition){
			case Condition.GreaterThan:
				return val > offValue;
			case Condition.LessThan:
				return val < offValue;
			default:
				return false;
			}
		}
	}
}

public class InputManager : MonoBehaviour {

	public InputAxisState[] inputs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var input in inputs){
			if(input.value)
				Debug.Log ("Input Detected: " + input.button);
		}
	}
}
