using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemy;
	public float spawnRate = 3f;
	public int maxEnemies;
	public Vector3[] spawnLocations;

	private float timeLastSpawned;
	private int enemiesSpawned;

	// Update is called once per frame
	void FixedUpdate () {
		if ((enemiesSpawned < maxEnemies) && (spawnRate <= Time.time - timeLastSpawned)) {
			SpawnEnemy(enemy);
		}
	
	}

	void SpawnEnemy (GameObject enemy)
	{
		int randomX = Random.Range (1, 5);
		GameObject newEnemy =  Instantiate (enemy, spawnLocations [enemiesSpawned % 2], Quaternion.identity) as GameObject;
		newEnemy.rigidbody2D.velocity = new Vector3 (randomX, 1, 0);
		timeLastSpawned = Time.time;
		enemiesSpawned++;
	}
}
