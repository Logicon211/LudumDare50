using UnityEngine;
using System.Collections.Generic;

public class PunchProjectile : MonoBehaviour {
  public float timeToLive = .5f;
  float currentTimeAlive;
  Stats stats;
  Punch punch;

  BoxCollider2D projectileBox;

  void Start() {
    currentTimeAlive = 0f;
    punch = GameObject.FindObjectOfType<Punch>();
    projectileBox = gameObject.GetComponent<BoxCollider2D>();
    CheckForHit();
  }

  void FixedUpdate() {
    currentTimeAlive += Time.deltaTime;
    if (currentTimeAlive >= timeToLive) {
      Destroy(gameObject);
    }
  }

  void SetStats(Stats newStats) {
    stats = newStats;
  }

  public void CheckForHit() {
    List<Collider2D> results = new List<Collider2D>();
    Transform punchTransform = this.transform;
    Physics2D.OverlapBox(
      new Vector2(punchTransform.position.x, punchTransform.position.y),
      projectileBox.size,
      0,
      new ContactFilter2D(),
      results
    );
    Debug.Log(results.Count);
    if (results.Count > 0) {
      foreach (Collider2D enemy in results) {
       punch.DoDamage(enemy.gameObject, punch.currentStats.damage);
      }
    }
  }

  // Probably will work, just a generic entry point that we might be able to hit to calculate damage
  // If we can get a generic summary of the players current bonuses, it'll make calculating damage faster
}