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
    Gizmos.matrix = projectileBox.transform.localToWorldMatrix;
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
    Vector3 worldCenter = projectileBox.transform.TransformPoint(projectileBox.offset);
    Vector3 worldHalfExtents = projectileBox.transform.TransformVector(projectileBox.size * 0.5f);
    Physics2D.OverlapBox(worldCenter, worldHalfExtents, 0f, new ContactFilter2D(), results);
    if (results.Count > 0) {
      float finalDamage = punch.GetDamage();
      foreach (Collider2D enemy in results) {
       punch.DoDamage(enemy.gameObject, finalDamage);
      }
    }
  }
}