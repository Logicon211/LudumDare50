using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthGained = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if (other.gameObject.tag == "Player") {
           //TODO: play sound on pickup    
           Player player = other.gameObject.GetComponent<Player>();
           player.Heal(healthGained);
           Destroy(this.gameObject);
        }
    }
}
