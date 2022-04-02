using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnLocations;

    public GameObject bear;
    public GameObject skeleton;
    public GameObject robot;
    public GameObject ninja;

    public GameObject finalBoss;
    // Start is called before the first frame update

    private float currentTime = 1f;
    public float spawnInterval = 10f;

    public Transform[] spawnPositions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0f) {
            // TODO, Ramp up spawn times as the game progresses
            currentTime = spawnInterval;

            int spawnPositionIndex = Random.Range(0, spawnPositions.Length);
            Instantiate(robot, new Vector3(spawnPositions[spawnPositionIndex].position.x, spawnPositions[spawnPositionIndex].position.y, spawnPositions[spawnPositionIndex].position.z), Quaternion.identity);
        }
    }

    public void SpawnFinalBoss() {
        // TODO: spawn big bad boy
    }
}
