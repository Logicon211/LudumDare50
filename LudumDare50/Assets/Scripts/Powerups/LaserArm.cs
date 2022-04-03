using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArm : MonoBehaviour
{
    public GameObject laserProjectile;
    
    // For growing laser:
    public GameObject laserPivot;
    public LaserProjectile projectile;

    public float delay = 0.8f;
    public float laserLifeTime = 3f;
    private float initialScaleX;

    private Laser laser;
    private float laserDamage = 0f;
    // Start is called before the first frame update
    void Start()
    {
        initialScaleX = this.transform.localScale.x;
    }

    public void setDamage(float damage) {
        laserDamage = damage;
    }

    public void setLaser(Laser laser) {
        this.laser = laser;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0f) {
            delay -= Time.deltaTime;
        } else {
            laserProjectile.SetActive(true);
            if(laser.GetLevel() >= 2 && laser.GetLevel() < 5) {
                laserPivot.transform.localScale = new Vector3(initialScaleX * 2f, laserPivot.transform.localScale.y, laserPivot.transform.localScale.z);
            } else if(laser.GetLevel() >= 5) {
                laserPivot.transform.localScale = new Vector3(initialScaleX * 2.8f, laserPivot.transform.localScale.y, laserPivot.transform.localScale.z);
            }
            projectile.setDamage(laserDamage);
            if(laserLifeTime > 0f) {
                laserLifeTime -= Time.deltaTime;
            } else {
                Destroy(this.gameObject);
            }
        }
    }
}
