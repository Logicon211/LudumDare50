using UnityEngine;
using System.Collections;

public class Punch : Powerup
{
  public BoxCollider2D rightPunch;
  public BoxCollider2D leftPunch;
  string[] allowedDirections = new string[]{"left", "right"};
  string currentDirection = "down";
  public GameObject punchProjectile;

  public override void UsePowerup() {

    StartCoroutine(StartPowerup(GetLevel()));
  }

  private IEnumerator StartPowerup(int level) {
    SpawnProjectile(false);
    if (level == 2) {
      yield return new WaitForSeconds(.5f);
      SpawnProjectile(true);
    }
    yield return null;
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