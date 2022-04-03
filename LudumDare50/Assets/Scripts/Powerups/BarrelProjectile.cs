using UnityEngine;
using System.Collections.Generic;

public class BarrelProjectile : MonoBehaviour {

    public GameObject explosion;
    public float timeToLive;
    public float timeTillExplosion;
    Player player;
    Barrel barrel;

  void Start() {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    barrel = GameObject.FindObjectOfType<Barrel>();
    timeTillExplosion = timeToLive;
  }

  void FixedUpdate() {
    timeTillExplosion -= Time.deltaTime;
    if (timeTillExplosion <= 0f) {
        CheckForHit();
        GameObject barrelExplosion = Instantiate(explosion, transform.position, new Quaternion(), barrel.gameArea.transform);
        Destroy(gameObject);
    }
  }

  void CheckForHit() {
      List<Collider2D> results = new List<Collider2D>();
      Physics2D.OverlapCircle(
          new Vector2(this.transform.position.x, this.transform.position.y),
          barrel.explosionRadius,
          new ContactFilter2D(),
          results
      );
      if (results.Count > 0) {
          float finalDamage = barrel.currentStats.damage + (12 * barrel.Level) * ( 1 + player.playerStats.damagePercentBonus);
          foreach(Collider2D enemy in results) {
              barrel.DoDamage(enemy.gameObject, finalDamage);
          }
      }
  }

}