using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

	public float lifeTime = 2f;
	public float aliveTime;
	public GameObject enemyProjectile;

	void Awake(){
		aliveTime = Time.time;
	}

	void FixedUpdate(){
		if (lifeTime >= Time.time - aliveTime) {
			Destroy(this.gameObject);
		}
	}
	
}
