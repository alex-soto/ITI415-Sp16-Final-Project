using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	/*
	 * New code: 
	 * 
	 * Overview: enemies will follow the player and attempt to hit them with ink projectiles. 
	 * If the enemy comes into contact with the player’s line, they will become friendly 
	 * (change color to give player feedback.) Friendly enemies will follow the player by default, 
	 * but will attack non-friendly enemies if the latter enters the former’s trigger radius.
	 */

	// If target != null

	public GameObject target;
	public bool isFriendly;
	public float moveDelay;
	public float attackDelay;
	public float moveSpeed;
//	public Color startColor = Color.red;
//	public Color friendlyColor = Color.black;
	public Sprite enemySprite;
	public Sprite friendlySprite;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public int numProjectiles = 6;

//	private GameObject _player;
	private Rigidbody2D rigidBody;
	private CheckForTarget checkForTarget;
	private float attackDistance;
	private float attackTime;
	private float moveTime;

	void Awake(){
		isFriendly = false;
		attackDistance = 25f;
		moveDelay = 0.5f;
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Start(){
		checkForTarget = gameObject.GetComponentInChildren<CheckForTarget>();
//		_player = GameObject.FindGameObjectWithTag("Player");
		checkForTarget.target = null;
	}

	void FixedUpdate(){
		target = checkForTarget.target;

		if ((checkForTarget.target != null) && (checkForTarget.targetInRange == true) && (moveDelay <= Time.time - moveTime)) {
			Move (target);
		}

		if (target != null) {
			// Behavior if a target has been set
			if ((attackDelay <= Time.time - attackTime) && (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)){
				Attack (target, numProjectiles);
			} else {
				Move (target);
			}
		} else {
			// Behavior if there is no target
			Idle ();
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (!isFriendly && col.transform.tag == "LineRendererGO") {
			isFriendly = true;
			GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ().IncreaseScore ();
			gameObject.tag = "Friendly";
			checkForTarget.target = null;
			gameObject.GetComponent<SpriteRenderer> ().sprite = friendlySprite;
		} else if (isFriendly && col.transform.tag == "Projectiles") {
			Destroy (this.gameObject);
		}
	}

//	void OnTriggerEnter2D(Collider2D col){
//		if (!isFriendly && col.gameObject.tag == "Player") {
//			target = col.gameObject;
//		} else if (isFriendly && col.gameObject.tag != "Player"){
//			target = col.gameObject;
//		}
//	}
//
//	void OnTriggerExit2D(Collider2D col){
//		CheckForTarget ();
//	}

	void Move(GameObject target){
		moveTime = Time.time; 
		Vector2 newMove = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
		rigidBody.velocity = newMove * moveSpeed;
	}
	
	void Attack(GameObject target, int numProjectiles){
		attackTime = Time.time;
		for (int i = numProjectiles; i > 0; i--) {
			GameObject proj = Instantiate(projectile, transform.position, Quaternion.FromToRotation(transform.position, target.transform.position)) as GameObject;
			Rigidbody2D projRigidBody = proj.GetComponent<Rigidbody2D> ();
			if (projRigidBody == null) {
				projRigidBody = proj.AddComponent<Rigidbody2D> ();
			}
			
			Vector2 projVelocity = new Vector2 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
			projRigidBody.velocity = projVelocity * projectileSpeed;
		}
	}
	
	void Idle(){
		
	}
	
	private Vector3 CalculateSafeJump(){
		//TODO: Delete the following temporary return value
		return Vector3.zero;
		
	}
	
	private void OnProjectileHit(){
		
	}
	
	private void OnLineHit(){
		
	}
}
