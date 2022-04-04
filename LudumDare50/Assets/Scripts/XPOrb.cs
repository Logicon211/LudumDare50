using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public float xpGained = 5f;

    private float movementSmoothing = 0.5f;
    private float magnetSpeed = 60f;
    private float magnetDistance = 3.5f;
    private GameObject player;
    private Rigidbody2D RB;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player) {
            float distanceToPlayer = Vector2.Distance(player.transform.position, this.transform.position);
            if(distanceToPlayer <= magnetDistance) {
                Vector3 normal = (player.transform.position - transform.position).normalized;
            
                Vector3 targetVelocity = new Vector2((normal.x * magnetSpeed * Time.fixedDeltaTime) * 10f, (normal.y * magnetSpeed * Time.fixedDeltaTime) * 10f);
                RB.velocity = Vector3.SmoothDamp(RB.velocity, targetVelocity, ref velocity, movementSmoothing);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if (other.gameObject.tag == "Player") {
           Player player = other.gameObject.GetComponent<Player>();
           player.GainXP(xpGained);
           Destroy(this.gameObject);
        }
    }
}
