using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	/*
	 * Old code:
	 * 
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
	 */

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
	public Color startColor = Color.red;
	public Color friendlyColor = Color.black;
	public GameObject projectile;
	public float projectileSpeed = 5f;
	public int numProjectiles = 6;

	private GameObject _player;
	private Rigidbody2D rigidBody;
	private CheckForPlayer CheckForPlayer;
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
		CheckForPlayer = gameObject.GetComponentInChildren<CheckForPlayer>();
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate(){

		if ((CheckForPlayer.playerInRange == true) && (moveDelay <= Time.time - moveTime)) {
			Move (_player);
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

	void OnTriggerEnter2D(Collider2D col){
		if (!isFriendly && col.gameObject.tag == "Player") {
			target = col.gameObject;
		} else if (isFriendly && col.gameObject.tag != "Player"){
			target = col.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		CheckForTarget ();
	}

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
	
	void CheckForTarget(){
		
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
