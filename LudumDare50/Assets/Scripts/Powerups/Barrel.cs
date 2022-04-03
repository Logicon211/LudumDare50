using UnityEngine;
using System.Collections;

public class Barrel : Powerup {

    public float spawnRadius;
    public GameObject barrelProjectile;
    // Dont want the bomb spawning near the player
    public float noSpawnArea;

    public float explosionRadius = 1.5f;

    // This is the one that gets set 
    public float actualExplosionRadius;

    protected override void Start()
    {
        base.Start();
        actualExplosionRadius = explosionRadius;
    }

    public override void UsePowerup() {
        StartCoroutine(StartPowerup());
    }

    private IEnumerator StartPowerup() {
        Debug.Log("about to spawn");
        for (int i = 0; i < currentStats.projectiles + player.playerStats.overdrive; i++) {
            SpawnProjectile();
            yield return new WaitForSeconds(currentStats.projectileSpeed);
        }
        yield return null;
    }

    public override void SetStats(int newLevel) {
        base.SetStats(newLevel);
        if (newLevel < 3) {
            actualExplosionRadius = explosionRadius;
        } else {
            actualExplosionRadius = explosionRadius * 2;
        }
    }

    // https://answers.unity.com/questions/1580130/i-need-to-instantiate-an-object-inside-a-donut-ins.html
    private Vector3 GetRandomPointInsideDonut(float innerRadius, float outerRadius) {
        float ratio = innerRadius / outerRadius;
        float radius = Mathf.Sqrt(Random.Range(ratio * ratio, 1f)) * outerRadius;
        return Random.insideUnitCircle.normalized * radius;
    }
    private void SpawnProjectile() {
        Debug.Log("spawning");
        Vector2 randomPoint = GetRandomPointInsideDonut(noSpawnArea, spawnRadius) + transform.position;

        GameObject barrel = Instantiate(barrelProjectile, gameArea.transform, true);
        barrel.transform.position = randomPoint;
    }
}