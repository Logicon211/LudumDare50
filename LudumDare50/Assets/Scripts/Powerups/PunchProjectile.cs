using UnityEngine;
using System.Collections.Generic;

public class PunchProjectile : MonoBehaviour {
  public float timeToLive = .5f;
  float currentTimeAlive;
  Stats stats;
  Punch punch;

  BoxCollider2D projectileBox;

  Player player;

  void Start() {
    currentTimeAlive = 0f;
    punch = GameObject.FindObjectOfType<Punch>();
    projectileBox = gameObject.GetComponent<BoxCollider2D>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
    float offset = projectileBox.offset.x;
    if (punch.GetDirection() == "left") {
      offset = -projectileBox.offset.x;
    }
    Physics2D.OverlapBox(
      new Vector2(punchTransform.position.x + offset, punchTransform.position.y),
      projectileBox.size,
      0,
      new ContactFilter2D(),
      results
    );
    if (results.Count > 0) {
      float finalDamage = punch.GetDamage();
      foreach (Collider2D enemy in results) {
       punch.DoDamage(enemy.gameObject, finalDamage);
      }
    }
  }
}