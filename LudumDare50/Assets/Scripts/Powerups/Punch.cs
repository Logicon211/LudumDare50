using UnityEngine;

public class Punch : Powerup
{
  public override void UsePowerup() {
    Debug.Log(GetPowerupLevelDescription(1));
  }

}