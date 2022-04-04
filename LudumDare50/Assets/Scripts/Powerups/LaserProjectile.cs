using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float currentAttackCooldown = 0f;
    // Start is called before the first frame update

    // Should be set when it first instantiates
    private float attackDamage = 1f;
    private float attackCooldown = 0.5f;

    private BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    public void setDamage(float damage) {
        attackDamage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        //Damage on cooldowns
        if(currentAttackCooldown > 0f) {
            currentAttackCooldown -= Time.deltaTime;
        } else {
            currentAttackCooldown = attackCooldown;
            ContactFilter2D filter = new ContactFilter2D();//.NoFilter();;

            LayerMask mask = LayerMask.GetMask("Enemies");
            filter.SetLayerMask(mask);
            filter.useTriggers = true;
            
            List<Collider2D> results = new List<Collider2D>();
            boxCollider.OverlapCollider(filter, results);
            foreach(Collider2D collision in results) {
                if (collision) {
                    if (collision.gameObject.tag == "Enemy") {
                        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
                        enemy.Damage(attackDamage);
                    }
                }
            }
        }
    }

}
