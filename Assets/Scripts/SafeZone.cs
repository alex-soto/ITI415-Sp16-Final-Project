using UnityEngine;
using System.Collections;

public class SafeZone : MonoBehaviour {

	public float rotateSpeed = 2.5f;
	
	private CircleCollider2D circleCollider;
	private ScoreManager scoreManager;

	void Awake(){
		circleCollider = GetComponent<CircleCollider2D> ();
		scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Debug.Log("Player reached the safezone!");
			scoreManager.GameOver(true);
		}
	}
}
