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

    SpawnProjectile(false);
    if (GetLevel() == 2) {
      SpawnProjectile(true);
    }
  }

  public void SpawnProjectile(bool reverse) {
    Vector3 spawn = new Vector3(playerObject.transform.position.x + 1.5f, playerObject.transform.position.y , 0);
    GameObject projectile = Instantiate(punchProjectile, player.transform);
    if (player.GetDirection() == "left" && !reverse || (reverse && player.GetDirection() == "right")) {
     spawn.x = spawn.x - 3f;
     projectile.transform.Rotate(new Vector3(0,180,0));
     }
    projectile.transform.position = spawn;
    projectile.transform.parent = gameArea.transform;
  }
}