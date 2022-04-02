using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Powerup
{
    public GameObject shotgun;
    public GameObject phaseInEffect;
    public override void UsePowerup() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = enemy;
                distance = curDistance;
            }
        }

        if(closest) {
            // Generating a random area slightly around the player instead of directly on top
            float xOffset = Random.Range(-2f, 2f);
            float yOffset = Random.Range(-2f, 2f);

            Vector3 offsetPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z);

            Vector3 direction = closest.transform.position - offsetPosition;
            direction.Normalize();

            // if (direction != Vector2.zero)
            // {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
            Quaternion postRotation = rotation * rotationAmount;
            // }
            Instantiate(phaseInEffect, offsetPosition, Quaternion.identity);
            GameObject floatingShotgun = Instantiate(shotgun, offsetPosition, postRotation);
            if (postRotation.eulerAngles.z > 90 && postRotation.eulerAngles.z < 270) {
                SpriteRenderer renderer = floatingShotgun.GetComponent<SpriteRenderer>();
                renderer.flipY = true;
            }
            floatingShotgun.GetComponent<FloatingShotgun>().setStats(currentStats.damage, currentStats.projectileSpeed, currentStats.projectiles);
            // return closest;
        }
    }
    public void CheckForHit() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
