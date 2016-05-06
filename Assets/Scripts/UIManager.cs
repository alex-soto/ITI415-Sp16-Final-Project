using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public GameObject uiText;
	//public GameObject uiImage;
	public GameObject objective;

	// Use this for initialization
	void Start () {

		objective = GameObject.Find ("SafeZone");
	}
	
	void FixedUpdate(){

		//float arrowRotation = Vector3.Angle (uiImage.transform.position, objective.transform.position);
		//uiImage.transform.eulerAngles = new Vector3 (0, 0, arrowRotation);
		//uiImage.transform.rotation = Quaternion.FromToRotation(transform.position, objective.transform.position);
		//uiImage.transform.rotation = Quaternion.Euler (1f, 1f, arrowRotation);
		//Quaternion.RotateTowards (uiImage.transform.rotation, objective.transform.rotation, 360f);
		//uiImage.transform.rotation = Quaternion.AngleAxis (arrowRotation, objective.transform.position);
	}
}
