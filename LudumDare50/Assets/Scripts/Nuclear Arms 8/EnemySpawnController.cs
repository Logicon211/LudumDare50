using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour {

	// public float xMin = -1f;
	// public float xMax = 1f;
	// public float zMin = -1f;
	// public float zMax = 1f;

	// public const int SKELETON_INDEX = 0;
	// public const int ROBOT_INDEX = 1;
	// public const int BEAR_INDEX = 2;

	public Transform [] spawnLocations;

	public float spawnInterval = 10f;
	public float currentTime = 50f;
	public GameObject enemy;
	public GameObject smokePoof;

	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(enemy != null) {
			currentTime -= Time.deltaTime;
			if(currentTime <= 0) {
				currentTime = spawnInterval;
				// Quaternion rotation = Quaternion.Euler (0f, 0f, 0f/*Random.Range (0f, 360f)*/);
				// Instantiate (enemy, testSpawnPosition.position, rotation);
				SpawnAtRandomLocation(enemy);
			}
		}
	}

	public void SpawnAtRandomLocation (GameObject enemyToSpawn) {
		Vector3 spawnLocation = PickSpawnPointNotOnPlayer();
		SpawnAtLocation (enemyToSpawn, spawnLocation.x, spawnLocation.y, spawnLocation.z);
	}

	private void SpawnAtLocation (GameObject enemyToSpawn, float xPos, float yPos, float zPos) {

		Vector3 position = new Vector3 (xPos, yPos, zPos);
		Quaternion rotation = Quaternion.Euler (0f, 0f, 0f/*Random.Range (0f, 360f)*/);
		Instantiate (smokePoof, position, rotation);
		Instantiate (enemyToSpawn, position, rotation);
	}


	public Vector3 PickSpawnPointNotOnPlayer() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
		}
		Transform closestPoint = null;
		foreach(Transform point in spawnLocations) {
			if(closestPoint == null || Vector3.Distance(point.position, player.transform.position) < Vector3.Distance(closestPoint.position, player.transform.position)) {
				closestPoint = point;
			}
		}

		Vector3 chosenPosition;
		bool spawnPointChosen = false;
		while (!spawnPointChosen) {
			chosenPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].position;

			if(!chosenPosition.Equals(closestPoint.position)) {
				return chosenPosition;
			}
		}

		return new Vector3();
	}

	public Vector3 PickSpawnPointFurthestFromPlayer() {
		if (player == null) {
			player = GameObject.FindGameObjectWithTag("Player");
		}
		Transform furthestPoint = null;
		foreach(Transform point in spawnLocations) {
			if(furthestPoint == null || Vector3.Distance(point.position, player.transform.position) > Vector3.Distance(furthestPoint.position, player.transform.position)) {
				furthestPoint = point;
			}
		}

		return new Vector3(furthestPoint.position.x, furthestPoint.position.y, furthestPoint.position.z);
	}
}
