using UnityEngine;
using System.Collections;

public class SafeZone : MonoBehaviour {

	public float rotateSpeed = 2.5f;

	private Rigidbody2D rigidBody;
	private CircleCollider2D circleCollider;

	void Awake(){
		rigidBody = GetComponent<Rigidbody2D> ();
		circleCollider = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		 
		rigidBody.rotation = rotateSpeed * Time.deltaTime;
	}
}
