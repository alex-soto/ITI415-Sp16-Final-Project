using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject followTarget;

	private Vector3 startingPos;

	void Start(){
		startingPos = transform.position;
	}

	void FixedUpdate(){
		Vector3 targetPos = followTarget.transform.position;
		Vector3 currentPos = this.transform.position;
		Vector3 newPos = new Vector3 (targetPos.x, targetPos.y, startingPos.z);
		transform.position = Vector3.Lerp (currentPos, newPos, 0.5f);
	}
}
