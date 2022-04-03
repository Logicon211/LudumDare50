using UnityEngine;
using System.Collections;

public class Barrel : Powerup {

    public float spawnRadius;
    public GameObject barrelProjectile;
    /** dont want it spawning on you **/
    public GameObject teleport;
    public float noSpawnArea;
    public float explosionRadius;
    
    public override void UsePowerup() {
        StartCoroutine(StartPowerup());
    }

    private IEnumerator StartPowerup() {
        for (int i = 0; i < currentStats.projectiles + player.playerStats.overdrive; i++) {
            SpawnProjectile();
            yield return new WaitForSeconds(currentStats.projectileSpeed);
        }
        yield return null;
    }
    
    // https://answers.unity.com/questions/1580130/i-need-to-instantiate-an-object-inside-a-donut-ins.html
    private Vector3 GetRandomPointInsideDonut(float innerRadius, float outerRadius) {
        float ratio = innerRadius / outerRadius;
        float radius = Mathf.Sqrt(Random.Range(ratio * ratio, 1f)) * outerRadius;
        return Random.insideUnitCircle.normalized * radius;
    }
    private void SpawnProjectile() {
        Vector2 randomPoint = GetRandomPointInsideDonut(noSpawnArea, spawnRadius) + transform.position;

        GameObject barrel = Instantiate(barrelProjectile, gameArea.transform, true);
        GameObject teleport = Instantiate(this.teleport, gameArea.transform, true);
        barrel.transform.position = randomPoint;
        teleport.transform.position = randomPoint;
    }


}