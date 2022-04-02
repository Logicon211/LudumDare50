using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Interface;


public class EnemyController: MonoBehaviour, IDamageable<float>, IKillable, IEnemy
{
    
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private float health = 10f;
    [SerializeField] private float attackDamage = 5f;

    public float speed = 10f;
    public Animator animator;

    public GameObject explosion;
    public GameObject poofEffect;
    public GameObject hitEffect;
	
    private AudioSource audio;
    private Rigidbody2D enemyBody;

    private GameObject player;

    private float currentHealth;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;

    public Transform explodeLocation;

    public GameObject[] debris;

    
    private Vector3 moveDir = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private float attackCooldown = 0.5f;
    private float currentAttackCooldown = 0f;
    private SpriteRenderer renderer;
    private void Awake() {
        enemyBody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        renderer = this.GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        Instantiate(poofEffect, transform.position, Quaternion.identity);
        currentHealth = health;
    }

     private void Update () {
        // float dist = Vector3.Distance(player.transform.position, transform.position);
        if (currentAttackCooldown > 0f) {
            currentAttackCooldown -= Time.deltaTime;
        }
        // if (dist > maxDistance) {
        //     move = true;
        // }
        // if (dist < attackDistance && currentAttackCooldown <= 0f)
        // {
        //     attack = true;
        // }

    }

    private void FixedUpdate() {
        Vector3 normal = (player.transform.position - transform.position).normalized;
        moveDir = normal;	
        Move(moveDir.x * speed * Time.fixedDeltaTime, moveDir.y * speed * Time.fixedDeltaTime);
        moveDir = Vector3.zero;
    }

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0f && !isDead)
            Kill();
        if (health > currentHealth) 
        {
            var healthPercentage = currentHealth/health;
            spriteRenderer.color = new Color(1f, healthPercentage, healthPercentage);
        }
    }
    
    public void Kill()
    {
        if(!isDead) {
            isDead = true;
            Instantiate(explosion, explodeLocation.position, Quaternion.identity);
            // if(roomController) {
            //     roomController.DecrementAliveEnemyCount();
            // }

            // float debrisForce = 1000f;
            // float debrisTorque = 500f;
            // CraigController craig = player.GetComponent<CraigController>();
            // if(craig.explodingEnemies) {
            //     Instantiate(explosion, explodeLocation.position, Quaternion.identity);
            //     //Damage other enemies here:
            //     debrisForce += 500f;
            //     debrisTorque += 500f;

            //     Collider2D[] hitColliders = Physics2D.OverlapCircleAll(explodeLocation.position, 8f);
            //     foreach(Collider2D collider in hitColliders) {
            //         IDamageable<float> damageable = collider.GetComponent<IDamageable<float>>();
            //         if(damageable != null) {
            //             damageable.Damage(craig.explodingEnemyDamage);
            //         }
            //     }
            // } else {
            //     Instantiate(poofEffect, explodeLocation.position, Quaternion.identity);
            // }
            // foreach(GameObject debrisPiece in debris) {
            //     GameObject part = Instantiate(debrisPiece, explodeLocation.position, Quaternion.identity);
            //     Rigidbody2D rb = part.GetComponent<Rigidbody2D>();
            //     Vector3 velocity = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            //     velocity.Normalize();
            //     rb.AddForce(velocity * debrisForce);
            //     rb.AddTorque(Random.Range(0f, debrisTorque));
            // }
            Destroy(gameObject);
        }
    }

    public void Move(float tarX, float tarY)
    {
        Vector3 targetVelocity = new Vector2(tarX * 10f, tarY * 10f);
        enemyBody.velocity = Vector3.SmoothDamp(enemyBody.velocity, targetVelocity, ref velocity, movementSmoothing);
        if (targetVelocity.x < 0f) {
            renderer.flipX = true;
        } else {
            renderer.flipX = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            if (currentAttackCooldown <= 0f) {
                Player player = other.gameObject.GetComponent<Player>();
                player.Damage(attackDamage);
                Instantiate(hitEffect, other.transform.position, Quaternion.identity);
                currentAttackCooldown = attackCooldown;
            }
        }
    }
    // void OnTriggerEnter2D(Collider2D other)
	// {   
    //     if (other.gameObject.tag == "Player") {
    //         CraigController craig = other.gameObject.GetComponent<CraigController>();
    //         craig.Damage(damage);
    //     }
	// 	Instantiate(hitEffect, transform.position, Quaternion.identity);
	// 	Destroy(gameObject);
	// }

}
