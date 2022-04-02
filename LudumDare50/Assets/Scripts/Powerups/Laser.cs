using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Powerup
{
    public GameObject laserArm;
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
            Vector3 direction = closest.transform.position - player.transform.position;
            direction.Normalize();

            // if (direction != Vector2.zero)
            // {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
            Quaternion postRotation = rotation * rotationAmount;
            // }
            Instantiate(phaseInEffect, player.transform.position, Quaternion.identity);
            GameObject createdArm = Instantiate(laserArm, player.transform.position, postRotation);
            if (postRotation.eulerAngles.z > 90 && postRotation.eulerAngles.z < 270) {
                SpriteRenderer renderer = createdArm.GetComponent<SpriteRenderer>();
                renderer.flipY = true;
            }
            createdArm.GetComponent<LaserArm>().setDamage(currentStats.damage);
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
