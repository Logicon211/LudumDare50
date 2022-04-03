using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalMove;
    private float verticalMove;

    private Rigidbody2D PlayerRigidBody;
    private Animator animator;
    // private Transform transform;
    public HealthBar healthbar;
    public XPBar xpbar;
    
    // private float maxPlayerHealth = 100f;
    private float currentPlayerHealth;

    private float xpNeededToLevel = 100f;
    private float currentPlayerXP = 0f;
    private float playerspeed = 5f;

    private GameManager gameManager;

    private AudioSource AS;
    private SpriteRenderer renderer;

    public AudioClip xpSound;
    public AudioClip levelUpSound;
    public AudioClip healSound;
    public AudioClip hurtSound;

    public bool lookingRight = true;

    public GameObject camera;
    [Serializable]    
    public struct PlayerStats{
        public float damagePercentBonus;
        public float cooldownPercentBonus;
        public int overdrive;

        public float maxPlayerHealth;
        public int armor;

        public float speedBonus;

    };
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        currentPlayerHealth = playerStats.maxPlayerHealth;
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        renderer = this.GetComponent<SpriteRenderer>();
        AS = this.GetComponent<AudioSource>();

        xpbar.SetXP(currentPlayerXP/xpNeededToLevel);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector3.Normalize(new Vector2(horizontalMove * playerspeed, verticalMove * playerspeed));
        PlayerRigidBody.velocity = (playerspeed * playerStats.speedBonus) * velocity;
        if (horizontalMove != 0f || verticalMove != 0f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (horizontalMove < 0f) {
            renderer.flipX = true;
            lookingRight = false;
            // transform.localScale = new Vector3(originalXScale * -1, transform.localScale.y, transform.localScale.z);
        } else if (horizontalMove > 0f) {
            renderer.flipX = false;
            lookingRight = true;
            // transform.localScale = new Vector3(originalXScale, transform.localScale.y, transform.localScale.z);
        }

        // // DEBUG LEVEL UP:
        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     gameManager.LevelUp();
        // }
    }

    void FixedUpdate()
    {
        //Get Movement unputs
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

    }

    public void Damage(float damage) {
        float damageAfterArmor = damage - playerStats.armor;
        if(damageAfterArmor < 0f) {
            damageAfterArmor = 0f;
        }
        currentPlayerHealth -= damageAfterArmor;

        AS.PlayOneShot(hurtSound);
        healthbar.SetHealth(currentPlayerHealth/playerStats.maxPlayerHealth);
    }

    public void Heal(float health) {
        currentPlayerHealth += health;
        if(healSound) {
            AS.PlayOneShot(healSound);
        }
        if(currentPlayerHealth > playerStats.maxPlayerHealth) {
            currentPlayerHealth = playerStats.maxPlayerHealth;
        }
        healthbar.SetHealth(currentPlayerHealth/playerStats.maxPlayerHealth);
    }

    public void GainXP(float xpGained) {
        currentPlayerXP += xpGained;
        if(xpSound) {
            AS.PlayOneShot(xpSound);
        }
        if (currentPlayerXP >= xpNeededToLevel) {
            if(levelUpSound) {
                AS.PlayOneShot(levelUpSound);
            }
            currentPlayerXP = currentPlayerXP - xpNeededToLevel;
            gameManager.LevelUp();
        }
        xpbar.SetXP(currentPlayerXP/xpNeededToLevel);
    }

    public float GetHealth() {
        return currentPlayerHealth;
    }

    public string GetDirection() {
        if (renderer.flipX) {
            return "left";
        }
        return "right";
    }

    public void DisableOnDeath() {
        camera.transform.parent = null;
        this.gameObject.SetActive(false);
    }
}
