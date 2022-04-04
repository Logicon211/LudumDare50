using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunProjectile : MonoBehaviour
{

    public List<GameObject> enemiesHit = new List<GameObject>();
    public float damage = 0f;

    public float timeToLive = 5f;

    public int numPenetrations = 3;
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
            if(!enemiesHit.Contains(other.gameObject)) {
                EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
                enemy.Damage(damage);
                if(numPenetrations <= 0) {
                    Destroy(this.gameObject);
                } else {
                    numPenetrations--;
                }
            }
        }
    }
}
