using UnityEngine;
using System.Collections;

public class CheckForPlayer : MonoBehaviour {

	public bool playerInRange = false;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			playerInRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			playerInRange = false;
		}
	}
}
