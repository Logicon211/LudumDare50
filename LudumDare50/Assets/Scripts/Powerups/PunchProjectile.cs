using UnityEngine;
using System.Collections.Generic;

public class PunchProjectile : MonoBehaviour {
  public float timeToLive = .5f;
  float currentTimeAlive;
  Stats stats;
  Punch punch;

  BoxCollider2D projectileBox;

  Player player;

  private List<GameObject> enemiesHit = new List<GameObject>();
  void Start() {
    currentTimeAlive = 0f;
    punch = GameObject.FindObjectOfType<Punch>();
    projectileBox = gameObject.GetComponent<BoxCollider2D>();
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    CheckForHit();
    // Gizmos.matrix = projectileBox.transform.localToWorldMatrix;
    if(punch.GetLevel() >= 3) {
      this.transform.localScale = new Vector3(this.transform.localScale.x * 2, this.transform.localScale.y * 2, this.transform.localScale.z * 2);
    }
  }

  void FixedUpdate() {
    currentTimeAlive += Time.deltaTime;
    if (currentTimeAlive >= timeToLive) {
      CheckForHit();
      Destroy(gameObject);
    }
  }

  void SetStats(Stats newStats) {
    stats = newStats;
  }

  public void CheckForHit() {
    List<Collider2D> results = new List<Collider2D>();
    // Vector3 worldCenter = projectileBox.transform.TransformPoint(projectileBox.offset);
    // Vector3 worldHalfExtents = projectileBox.transform.TransformVector(projectileBox.size * 0.5f);
    ContactFilter2D filter = new ContactFilter2D();//.NoFilter();;

    LayerMask mask = LayerMask.GetMask("Enemies");
    filter.SetLayerMask(mask);
    filter.useTriggers = true;
    // filter.SetDepth(-100f, 100f);
    
    // List<Collider2D> results = new List<Collider2D>();
    projectileBox.OverlapCollider(filter, results);
    foreach(Collider2D enemy in results) {
        if (enemy) {
            if (enemy.gameObject.tag == "Enemy") {
                if(!enemiesHit.Contains(enemy.gameObject)) {
                  float finalDamage = punch.GetDamage();
                  punch.DoDamage(enemy.gameObject, finalDamage);
                  enemiesHit.Add(enemy.gameObject);
                }
            }
        }
    }
    // Physics2D.OverlapBox(worldCenter, worldHalfExtents, 0f, filter, results);
    // if (results.Count > 0) {
    //   float finalDamage = punch.GetDamage();
    //   foreach (Collider2D enemy in results) {
    //    Debug.Log("Found Enemy in collision: " + enemy.gameObject);
    //    punch.DoDamage(enemy.gameObject, finalDamage);
    //   }
    // }
  }
}