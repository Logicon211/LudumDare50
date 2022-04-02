using UnityEngine;
using System.Collections.Generic;

public class Punch : Powerup
{
  public BoxCollider2D rightPunch;
  public BoxCollider2D leftPunch;
  string[] allowedDirections = new string[]{"left", "right"};
  string currentDirection = "down";
  public GameObject punchProjectile;

  public override void UsePowerup() {
    Debug.Log("testestestest");
    CheckForHit();
  }

  public void SpawnProjectile() {
    Instantiate(punchProjectile, rightPunch.transform);
  }
  public void CheckForHit() {
    List<Collider2D> results = new List<Collider2D>();
    Transform punchTransform = rightPunch.transform;
    Physics2D.OverlapBox(
      new Vector2(punchTransform.position.x, punchTransform.position.y),
      rightPunch.size,
      0,
      new ContactFilter2D(),
      results
    );
    if (results.Count > 0) {
      foreach (Collider2D enemy in results) {
        DoDamage(enemy.gameObject, currentStats.damage);
      }
    }
  }

}