using UnityEngine;

public class Punch : Powerup
{
  public BoxCollider2D rightPunch;
  public BoxCollider2D leftPunch;
  string[] allowedDirections = new string[]{"left", "right"};
  string currentDirection = "down";

  public override void UsePowerup() {
    
  }
  public void CheckForHit() {

  }

}