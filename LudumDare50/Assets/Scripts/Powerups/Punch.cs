using UnityEngine;

public class Punch : Powerup
{
  public override void UsePowerup() {
    LevelUp(1);
    Debug.Log(GetPowerupLevelDescription(1));
  }

}