using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float startingMoveSpeed;
	public float jumpMultiplier;
	public float lineSpeedMultiplier;
	public int startingJumpCount;
	public float radius = 5f;
	public LayerMask layerMask;
	public bool isStanding;

	private float moveSpeed;
	private int jumpCount;
	private bool canJump = false;
	private float startingPlayerGravity;
	private Rigidbody2D rigidBody;
	private bool hasJumped = false;
	private float jumpDelay = 0.2f;
	private float timeJumped;
	private float timeUntilJumpReset;

	private ScoreManager scoreManager;

	void Awake(){
		startingMoveSpeed = 15f;
		moveSpeed = startingMoveSpeed;
		startingJumpCount = 2;
		jumpCount = startingJumpCount;
		jumpMultiplier = 2f;
		lineSpeedMultiplier = 2f;
		startingPlayerGravity = 2f;
		Vector2 pos = transform.position;
		rigidBody = GetComponent<Rigidbody2D>();
		//playerColl = GetComponent<CircleCollider2D> ();
		//StrokeManager strokeManager = GetComponent<StrokeManager>();
	}

	void Start(){
		rigidBody.gravityScale = startingPlayerGravity;
		scoreManager = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
	}

	void FixedUpdate(){

		float horizontal = Input.GetAxisRaw ("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");

		radius = transform.localScale.x / 4f;
		Vector2 bottomPos = new Vector2 (transform.position.x, transform.position.y - (transform.localScale.y / 2));
		isStanding = Physics2D.OverlapCircle (bottomPos, radius, layerMask);

		if (horizontal != 0) {
			Move (horizontal);
		}

		if (vertical > 0 && canJump) {
			Jump (vertical);
		}

		// Allows player to jump when standing on a solid surface
		if (isStanding) {
			canJump = true;
			jumpCount = startingJumpCount;
			moveSpeed = startingMoveSpeed;
		}

	}

	void Move(float horizontal){
		Vector2 vel = rigidBody.velocity;
		rigidBody.velocity = new Vector2 (moveSpeed * horizontal, vel.y);
	}

	void Jump(float vertical){

		if (jumpCount <= 0){
			canJump = false;
			return;
		}

		if (!hasJumped) {
			timeJumped = Time.time;
			hasJumped = true;
		}

		if (hasJumped && Time.time >= jumpDelay + timeJumped) {
			hasJumped = false;
		}

		if (!hasJumped && jumpCount > 0) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, jumpMultiplier * moveSpeed);
			jumpCount--;
		}

	}

	IEnumerable KillPlayer(){
		Destroy (this.gameObject);
		yield return new WaitForSeconds (2);
		Application.LoadLevel(Application.loadedLevelName);
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.collider.tag == "LineRendererGO") {
			jumpCount = 2;
			canJump = true;
			moveSpeed *= lineSpeedMultiplier;
		} else if (coll.collider.tag == "Enemy" || coll.collider.tag == "Projectiles"){
			StartCoroutine("KillPlayer");
			scoreManager.GameOver(false);
		}

	}

	void OnCollisionStay2d(Collision2D coll){
		if (coll.collider.tag == "LineRendererGO") {
			rigidBody.gravityScale = 0f;
		}
	}

	void OnCollisionExit2D(Collision2D coll){
		moveSpeed = startingMoveSpeed;
		rigidBody.gravityScale = startingPlayerGravity;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;

		Vector3 leftPos = new Vector3(transform.position.x - (transform.localScale.x / 2), 
		                          transform.position.y, 1);
		Vector3 rightPos = new Vector3(transform.position.x + (transform.localScale.x / 2), 
		                              transform.position.y, 1);
		Vector3 bottomPos = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 2), 1);;
		// Vector3 pos = new Vector3(standingX, standingY, 1);
		Gizmos.DrawWireSphere (leftPos, radius);
		Gizmos.DrawWireSphere (rightPos, radius);
		Gizmos.DrawWireSphere (bottomPos, radius);
	}

}
