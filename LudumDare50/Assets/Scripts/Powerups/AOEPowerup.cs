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
                    float finalDamage = GetDamage();
                    enemy.Damage(finalDamage);
                }
            }
        }
    }
    public void CheckForHit() {

    }

    // Update is called once per frame
    void Update()
    {
        float overdriveIncrease = 0.08f * player.playerStats.overdrive;
        if (level == 1) {
            this.transform.localScale = new Vector3(initialScale.x * (1f + overdriveIncrease), initialScale.y * (1f + overdriveIncrease), initialScale.z * (1f + overdriveIncrease));
        } else if (level == 2) {
            this.transform.localScale = new Vector3(initialScale.x * (1.18f + overdriveIncrease), initialScale.y * (1.18f + overdriveIncrease), initialScale.z * (1.18f + overdriveIncrease));
        } else if(level == 3) {
            this.transform.localScale = new Vector3(initialScale.x * (1.25f + overdriveIncrease), initialScale.y * (1.25f + overdriveIncrease), initialScale.z * (1.25f + overdriveIncrease));
        } else if(level >= 4 && level < 6) {
            this.transform.localScale = new Vector3(initialScale.x * (1.33f + overdriveIncrease), initialScale.y * (1.33f + overdriveIncrease), initialScale.z * (1.33f + overdriveIncrease));
        } else if(level >= 6 && level < 8) {
            this.transform.localScale = new Vector3(initialScale.x * (1.5f + overdriveIncrease), initialScale.y * (1.5f + overdriveIncrease), initialScale.z * (1.5f + overdriveIncrease));
        } else if(level >= 8) {
            this.transform.localScale = new Vector3(initialScale.x * (1.7f + overdriveIncrease), initialScale.y * (1.7f + overdriveIncrease), initialScale.z * (1.7f + overdriveIncrease));
        }
        if(active) {
            radiation.SetActive(true);
        }
    }

    void Start() {
        base.Start();
        initialScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }

    public override bool isUtilityPowerup() {
        return false;
    }
}
