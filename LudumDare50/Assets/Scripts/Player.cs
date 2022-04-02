using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float horizontalMove;
    private float verticalMove;

    private Rigidbody2D PlayerRigidBody;
    private Animator animator;
    // private Transform transform;
    public HealthBar healthbar;
    
    private float maxPlayerHealth = 100f;
    private float currentPlayerHealth;
    private float playerspeed = 5f;
    private float originalXScale;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        originalXScale = transform.localScale.x;
        currentPlayerHealth = maxPlayerHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector3.Normalize(new Vector2(horizontalMove * playerspeed, verticalMove * playerspeed));
        PlayerRigidBody.velocity = playerspeed * velocity;
        if (horizontalMove != 0f || verticalMove != 0f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (horizontalMove < 0f) {
            transform.localScale = new Vector3(originalXScale * -1, transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(originalXScale, transform.localScale.y, transform.localScale.z);
        }
        
        // DEBUG LEVEL UP:
        if (Input.GetKeyDown(KeyCode.Space)) {
            gameManager.LevelUp();
        }
    }

    void FixedUpdate()
    {
        //Get Movement unputs
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

    }

    public void Damage(float damage) {
        currentPlayerHealth -= damage;
        if (currentPlayerHealth <= 0) {
            
        }
        // health -= damageTaken;
        // if(damageTaken > 0f) {
        //     AS.PlayOneShot(hurtSound);
        // }
        healthbar.SetHealth(currentPlayerHealth/maxPlayerHealth);
    }

    public float GetHealth() {
        return currentPlayerHealth;
    }
}
