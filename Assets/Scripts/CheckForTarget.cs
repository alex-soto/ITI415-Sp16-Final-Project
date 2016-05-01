using UnityEngine;
using System.Collections;

public class CheckForTarget : MonoBehaviour {

	public GameObject target;
	public bool targetInRange = false;

	void OnTriggerEnter2D(Collider2D other){
		if ((transform.parent.tag == "Enemy" && other.tag == "Player") || 
		    (transform.parent.tag == "Friendly" && other.collider2D.tag == "Enemy")) {
			targetInRange = true;
			target = other.gameObject;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if ((transform.parent.tag == "Enemy" && other.tag == "Player") || 
		    (transform.parent.tag == "Friendly" && other.tag == "Enemy")) {
			targetInRange = false;
			target = null;
		}
	}
}
