using UnityEngine;
using System.Collections.Generic;

public class BarrelProjectile : MonoBehaviour {

    public GameObject explosion;
    public GameObject teleport;
    public GameObject circle;
    public float timeToLive;
    public float timeTillExplosion;
    public float markerTimer;
    Player player;
    Barrel barrel;
    Animator animator;
    bool hasAppeared;
    SpriteRenderer sprite;

  void Start() {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    barrel = GameObject.FindObjectOfType<Barrel>();
    sprite = gameObject.GetComponent<SpriteRenderer>();
    animator = gameObject.GetComponentInChildren<Animator>();
    timeTillExplosion = timeToLive + markerTimer;
    if (barrel.GetLevel() > 2) {
        sprite.gameObject.transform.localScale *= 2; 
    }
  }

  void FixedUpdate() {
    timeTillExplosion -= Time.deltaTime;
    if (!hasAppeared && timeTillExplosion <= timeToLive - markerTimer) {
        animator.SetBool("ShowBarrel", true);
        hasAppeared = true;
        Instantiate(teleport, transform.position, new Quaternion(), barrel.gameArea.transform);
    }
    if (timeTillExplosion <= 0f) {
        CheckForHit();
        GameObject barrelExplosion = Instantiate(explosion, transform.position, new Quaternion(), barrel.gameArea.transform);
        if (barrel.GetLevel() > 2) {
            barrelExplosion.gameObject.transform.localScale *= 2; 
        }
        Destroy(gameObject);
    }
  }

  void CheckForHit() {
      List<Collider2D> results = new List<Collider2D>();
      Physics2D.OverlapCircle(
          new Vector2(this.transform.position.x, this.transform.position.y),
          barrel.actualExplosionRadius * 2f,
          new ContactFilter2D(),
          results
      );
      if (results.Count > 0) {
          Debug.Log(results.Count);
          float finalDamage = barrel.currentStats.damage + (12 * barrel.Level) * ( 1 + player.playerStats.damagePercentBonus);
          foreach(Collider2D enemy in results) {
              barrel.DoDamage(enemy.gameObject, finalDamage);
          }
      }
  }

}