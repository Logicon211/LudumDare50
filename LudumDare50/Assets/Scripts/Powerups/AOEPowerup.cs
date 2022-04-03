using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEPowerup : Powerup
{
    // public GameObject aoeEffect;

    public Collider2D aoeCollider;
    private Vector3 initialScale;

    public GameObject radiation;
    public override void UsePowerup() {
        ContactFilter2D filter = new ContactFilter2D();//.NoFilter();;

        LayerMask mask = LayerMask.GetMask("Enemies");
        filter.SetLayerMask(mask);
        
        List<Collider2D> results = new List<Collider2D>();
        aoeCollider.OverlapCollider(filter, results);
        foreach(Collider2D collision in results) {
            if (collision) {
                if (collision.gameObject.tag == "Enemy") {
                    EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                    enemy.Damage(currentStats.damage);
                }
            }
        }
        // GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        // GameObject closest = null;
        // float distance = Mathf.Infinity;
        // Vector3 position = transform.position;
        // foreach (GameObject enemy in enemies)
        // {
        //     Vector3 diff = enemy.transform.position - position;
        //     float curDistance = diff.sqrMagnitude;
        //     if (curDistance < distance)
        //     {
        //         closest = enemy;
        //         distance = curDistance;
        //     }
        // }

        // if(closest) {
        //     // Generating a random area slightly around the player instead of directly on top
        //     float xOffset = Random.Range(-2f, 2f);
        //     float yOffset = Random.Range(-2f, 2f);

        //     Vector3 offsetPosition = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z);

        //     Vector3 direction = closest.transform.position - offsetPosition;
        //     direction.Normalize();

        //     // if (direction != Vector2.zero)
        //     // {
        //     Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        //     Quaternion rotationAmount = Quaternion.Euler(0, 0, 90);
        //     Quaternion postRotation = rotation * rotationAmount;
        //     // }
        //     Instantiate(phaseInEffect, offsetPosition, Quaternion.identity);
        //     GameObject floatingShotgun = Instantiate(shotgun, offsetPosition, postRotation);
        //     if (postRotation.eulerAngles.z > 90 && postRotation.eulerAngles.z < 270) {
        //         SpriteRenderer renderer = floatingShotgun.GetComponent<SpriteRenderer>();
        //         renderer.flipY = true;
        //     }
        //     floatingShotgun.GetComponent<FloatingShotgun>().setStats(currentStats.damage, currentStats.projectileSpeed, currentStats.projectiles);
        //     // return closest;
        // }
    }
    public void CheckForHit() {

    }

    // Update is called once per frame
    void Update()
    {
        if (level == 2) {
            this.transform.localScale = new Vector3(initialScale.x * 1.18f, initialScale.y * 1.18f, initialScale.z * 1.18f);
        } else if(level >= 3) {
            this.transform.localScale = new Vector3(initialScale.x * 1.25f, initialScale.y * 1.25f, initialScale.z * 1.25f);
        }
        if(active) {
            radiation.SetActive(true);
        }
    }

    void Start() {
        base.Start();
        initialScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }
}
