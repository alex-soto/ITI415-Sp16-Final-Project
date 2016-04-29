using UnityEngine;
using System.Collections;

public class DestroyObjects : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Destroying " + other.name);
		Destroy (other);
	}
}
