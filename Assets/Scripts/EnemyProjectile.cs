using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public float lifeTime = 3f;
	private float aliveTime;
	private GameObject enemyProjectile;

	void Awake(){
		aliveTime = Time.time;
		enemyProjectile = this.gameObject;
	}

	void FixedUpdate(){
		if (lifeTime >= Time.time - aliveTime) {
			Destroy(enemyProjectile);
		}
	}
	
}
