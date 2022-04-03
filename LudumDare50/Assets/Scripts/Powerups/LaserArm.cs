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

    private float laserDamage = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setDamage(float damage) {
        laserDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0f) {
            delay -= Time.deltaTime;
        } else {
            laserProjectile.SetActive(true);
            projectile.setDamage(laserDamage);
            if(laserLifeTime > 0f) {
                laserLifeTime -= Time.deltaTime;
            } else {
                Destroy(this.gameObject);
            }
        }
    }
}
