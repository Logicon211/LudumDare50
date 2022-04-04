using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Powerup
{
    public GameObject laserArm;
    public GameObject phaseInEffect;
    public override void UsePowerup() {
        int numberOfShots = 1 + Mathf.CeilToInt(player.playerStats.overdrive/2);
        for(int i = 0; i <= numberOfShots; i++) {
            float delay = i*0.3f;
            IEnumerator coroutine = FireLaser(delay);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator FireLaser(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        // Generating a random area slightly around the player instead of directly on top
        float xOffset = Random.Range(-2f, 2f);
        float yOffset = Random.Range(-2f, 2f);

        Vector3 offsetPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z);
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = offsetPosition;
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

            Vector3 direction = closest.transform.position - offsetPosition;
            direction.Normalize();

            // if (direction != Vector2.zero)
            // {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
            Quaternion postRotation = rotation * rotationAmount;
            // }
            Instantiate(phaseInEffect, offsetPosition, Quaternion.identity);
            GameObject createdArm = Instantiate(laserArm, offsetPosition, postRotation);
            if (postRotation.eulerAngles.z > 90 && postRotation.eulerAngles.z < 270) {
                SpriteRenderer renderer = createdArm.GetComponent<SpriteRenderer>();
                renderer.flipY = true;
            }
            float finalDamage = GetDamage();
            createdArm.GetComponent<LaserArm>().setDamage(finalDamage);
            createdArm.GetComponent<LaserArm>().setLaser(this);
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
