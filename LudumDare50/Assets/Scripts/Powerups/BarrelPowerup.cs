using UnityEngine;
using System.Collections;

public class BarrelPowerup : Powerup {

    public float spawnRadius;
    public GameObject barrelProjectile;
    /** dont want it spawning on you **/
    public float noSpawnArea;
    void UsePowerup() {
        StartCoroutine(StartPowerup());
    }

    private IEnumerator StartPowerup() {
        for (int i = 0; i < currentStats.projectiles + player.playerStats.overdrive; i++) {
            SpawnProjectile();
            yield return new WaitForSeconds(currentStats.projectileSpeed);
        }
        yield return null;
    }

    private void SpawnProjectile() {
        Vector2 randomPoint = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPoint = new Vector3(randomPoint.x, randomPoint.y);
        GameObject barrel = Instantiate(barrelProjectile, gameArea.transform, true);
        barrel.transform.position = spawnPoint;
    }


}