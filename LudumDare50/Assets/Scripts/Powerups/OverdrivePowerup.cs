using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdrivePowerup : Powerup
{
    public int overdriveIncrease = 1;
    public override void UsePowerup() {

    }
    public void CheckForHit() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetStats(int newLevel) {
        // DO nothing
    }

    public override void LevelUp(int numberOfLevels) {
        // if (level >= levelStats.Length) {
        //     Debug.Log("Cannot level up anymore, already at max level");
        //     return;
        // }
        level += numberOfLevels;
        player.playerStats.overdrive += overdriveIncrease;
    }
}
