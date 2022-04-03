using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : MonoBehaviour
{
    public float damage = 0f;

    public float timeToLive = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToLive > 0f) {
            timeToLive -= Time.deltaTime;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
