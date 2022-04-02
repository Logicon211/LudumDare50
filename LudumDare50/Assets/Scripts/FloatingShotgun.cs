using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingShotgun : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    public Transform shootPosition;

    public float delay = 0.5f;
    public float shotgunLifeTime = 1f;

    private float damage = 0f;
    private float projectileSpeed = 0f;
    private int numberOfProjectiles = 0;

    public float accuracy = 8f;
    private bool fired = false;

    private Animator animator;
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void setStats(float damageIn, float projectileSpeedIn, int numberOfProjectilesIn) {
        this.damage = damageIn;
        this.projectileSpeed = projectileSpeedIn;
        this.numberOfProjectiles = numberOfProjectilesIn;
        Debug.Log("SPEED SET: " + (this.projectileSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0f) {
            delay -= Time.deltaTime;
        } else {
            //TODO: Shoot gun
            // projectile.setDamage(laserDamage);
            if(!fired) {
                for (int i = 0; i<numberOfProjectiles; i++) {
                    // TODO: Trigger fire animation
                    GameObject projectileLaunched = Instantiate(bullet, shootPosition.position, shootPosition.rotation) as GameObject;
                    projectileLaunched.GetComponent<ShotgunProjectile>().damage = damage;
                    projectileLaunched.transform.Rotate(0, 0, Random.Range(-accuracy, accuracy));
                    projectileLaunched.GetComponent<Rigidbody2D>().velocity = projectileLaunched.transform.right * (projectileSpeed);
                    
                    fired = true;
                    animator.SetBool("Shoot", true);
                    // Debug.Log("SHOTGUN SHOOTING AT: " + (projectileSpeed));
                }
            } else {
                animator.SetBool("Shoot", false);
            }
            if(shotgunLifeTime > 0f) {
                shotgunLifeTime -= Time.deltaTime;
            } else {
                Destroy(this.gameObject);
            }
        }
    }
}
