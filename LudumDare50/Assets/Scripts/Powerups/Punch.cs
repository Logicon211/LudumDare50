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
    SpawnProjectile();
  }

  public void SpawnProjectile() {
    GameObject projectile = Instantiate(punchProjectile, rightPunch.transform);
    projectile.transform.parent = gameArea.transform;
  }
}