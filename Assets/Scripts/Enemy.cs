using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isFriendly;
	public bool isAttacking = false;
	public GameObject attackTarget;
	public float attackDistance;
	public float moveSpeed = 15f;
	public float moveDelay = 5f;
	public float jumpDelay = 5f;

	private bool isMoving;
	private bool hasJumped;
	private float jumpTime;
	private float lastTimeMoved = 0;
	private Rigidbody2D rigidBody;

	void Awake(){
		rigidBody = GetComponent<Rigidbody2D> ();
		attackDistance = 25f;
	}

	void Start(){
		isFriendly = false;
		isMoving = false;
		hasJumped = false;
		attackTarget = null;
	}

	void FixedUpdate(){

		if (moveDelay >= Time.time - lastTimeMoved) {
			if (isAttacking){
				Attack (attackTarget);
			} else {
				Move ();
			}
		}

	}

	void OnTriggerEnter2D(Collider2D col){
		Debug.Log("Enemy triggered");
		if (!isFriendly && col.gameObject.tag == "Player") {
			isAttacking = true;
			attackTarget = GameObject.FindGameObjectWithTag("Player");
			Move (attackTarget);
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (isAttacking){
			isAttacking = false;
		}
	}

	void Attack(GameObject target){
		Vector2 targetPos = (Vector2) target.transform.position;
	}

	void Move(){

	}

	void Move(GameObject target){
		if (jumpDelay >= Time.time - jumpTime){
			hasJumped = false;
			jumpTime = Time.time;
		}

		Vector3 targetPos = target.transform.position - transform.position;
		if (hasJumped) {
			targetPos.y = 0;
		}

		targetPos.Normalize ();
		rigidBody.velocity = new Vector3 (targetPos.x * moveSpeed, targetPos.y * moveSpeed, transform.position.z);
		isMoving = true;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackDistance);
	}
}
