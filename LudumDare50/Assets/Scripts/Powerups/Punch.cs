using UnityEngine;
using System.Collections;

public class Punch : Powerup
{
  public GameObject punchProjectile;
  public float spawnDelay = .2f;

  public override void UsePowerup() {

    StartCoroutine(StartPowerup(GetLevel()));
  }

  private IEnumerator StartPowerup(int level) {
    bool mirrorAttack = false;
    for (int i = 0; i < currentStats.projectiles + player.playerStats.overdrive ; i++) {
      SpawnProjectile(mirrorAttack);
      mirrorAttack = !mirrorAttack;
      yield return new WaitForSeconds(spawnDelay);
    }
    yield return null;
  }

  public void SpawnProjectile(bool reverse) {
    Vector3 spawn = new Vector3(playerObject.transform.position.x + 1.5f, playerObject.transform.position.y , 0);
    GameObject projectile = Instantiate(punchProjectile, player.transform, true);
    if (player.GetDirection() == "left" && !reverse || (reverse && player.GetDirection() == "right")) {
     spawn.x = spawn.x - 3f;
     projectile.transform.Rotate(new Vector3(0,180,0));
     }
    projectile.transform.position = spawn;
    projectile.transform.parent = gameArea.transform;
  }

  public string GetDirection() {
    return player.GetDirection();
  }
}