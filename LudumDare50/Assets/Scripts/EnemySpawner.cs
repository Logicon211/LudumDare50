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

    private float currentSkeletonTime = 4f;
    private float skeletonSpawnInterval = 4f;
    private float currentBearTime = 6f;
    private float bearSpawnInterval = 6f;
    private float currentRobotTime = 5f;
    private float robotSpawnInterval = 5f;
    private float currentNinjaTime = 7f;
    private float ninjaSpawnInterval = 7f;

    private float timeElapsed = 0f;
    public Transform[] spawnPositions;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        currentSkeletonTime -= Time.deltaTime;
        if(currentSkeletonTime <= 0f) {
            // TODO, Ramp up spawn times as the game progresses
            currentSkeletonTime = skeletonSpawnInterval;

            float healthBoost = timeElapsed/300f;
            float damageBoost = timeElapsed/600f;
            float speedBoost = timeElapsed/2000f;

            int skeletonGroup = 2 + Mathf.FloorToInt(timeElapsed/60f);
            for(int i = 0; i < skeletonGroup; i++) {
                int spawnPositionIndex = Random.Range(0, spawnPositions.Length);
                EnemyController enemy = Instantiate(skeleton, spawnPositions[spawnPositionIndex].position, Quaternion.identity).GetComponent<EnemyController>();
                enemy.setTimeIncreaseStats(damageBoost, healthBoost, speedBoost);
            }
        }
        if (timeElapsed > 60f) {
            currentRobotTime -= Time.deltaTime;
            if(currentRobotTime <= 0f) {
                currentRobotTime = robotSpawnInterval;

                float healthBoost = timeElapsed/150f;
                float damageBoost = timeElapsed/300f;
                float speedBoost = timeElapsed/1500f;

                int robotGroup = 2 + Mathf.FloorToInt(timeElapsed/90f);
                for(int i = 0; i < robotGroup; i++) {
                    int spawnPositionIndex = Random.Range(0, spawnPositions.Length);
                    EnemyController enemy = Instantiate(robot, spawnPositions[spawnPositionIndex].position, Quaternion.identity).GetComponent<EnemyController>();
                    enemy.setTimeIncreaseStats(damageBoost, healthBoost, speedBoost);
                }
            }
        }

        if (timeElapsed > 120f) {
            currentBearTime -= Time.deltaTime;
            if(currentBearTime <= 0f) {
                currentBearTime = bearSpawnInterval;

                float healthBoost = timeElapsed/120f;
                float damageBoost = timeElapsed/200f;
                float speedBoost = timeElapsed/1000f;

                int bearGroup = 2 + Mathf.FloorToInt(timeElapsed/100f);
                for(int i = 0; i < bearGroup; i++) {
                    int spawnPositionIndex = Random.Range(0, spawnPositions.Length);
                    EnemyController enemy = Instantiate(bear, spawnPositions[spawnPositionIndex].position, Quaternion.identity).GetComponent<EnemyController>();
                    enemy.setTimeIncreaseStats(damageBoost, healthBoost, speedBoost);
                }
            }
        }

        if (timeElapsed > 180f) {
            currentNinjaTime -= Time.deltaTime;
            if(currentNinjaTime <= 0f) {
                currentNinjaTime = ninjaSpawnInterval;

                float healthBoost = timeElapsed/300f;
                float damageBoost = timeElapsed/600f;
                float speedBoost = timeElapsed/2000f;

                int ninjaGroup = 2 + Mathf.FloorToInt(timeElapsed/120f);
                for(int i = 0; i < ninjaGroup; i++) {
                    int spawnPositionIndex = Random.Range(0, spawnPositions.Length);
                    EnemyController enemy = Instantiate(ninja, spawnPositions[spawnPositionIndex].position, Quaternion.identity).GetComponent<EnemyController>();
                    enemy.setTimeIncreaseStats(damageBoost, healthBoost, speedBoost);
                }
            }
        }
    }

    public void SpawnFinalBoss() {
        // TODO: spawn big bad boy
    }
}
