using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerup : Powerup
{
    public float speedIncrease = 0.2f;
    public override void UsePowerup() {

    }
    public void CheckForHit() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetStats(int newLevel) {
        Debug.Log("Calling SpeedPowerup Set stats instead...");
        // DO nothing
    }

    public override void LevelUp(int numberOfLevels) {
        // if (level >= levelStats.Length) {
        //     Debug.Log("Cannot level up anymore, already at max level");
        //     return;
        // }
        level += numberOfLevels;
        player.playerStats.speedBonus += speedIncrease;
    }
}
