using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBase: MonoBehaviour, IDamageable<float>, IKillable
{
    public float health = 100f;
    public float speed = 1f;
    public float attackRange = 5f;

    public string targetTag = "Player";
    public string attackScriptName;
    
    public GameObject target;
    public AudioSource oneshotAudioSource;
    public AudioClip deathAudio;

    float currentHealth;
    float currentAttackCooldown = 0f;

    Transform targetTransform;
    Animator animator;
    IAttack<Animator, GameObject> attack;

    public bool testKill = false;
    public bool testDamage = false;

    private bool kinematic = false;
    private float kinematicCoolDown = 0.5f;
    private float currentKinematicTime = 0f;

    private Rigidbody rigidbody;

    void Start()
    {
        currentHealth = health;
        attack = GetComponent<IAttack<Animator, GameObject>>();
        animator = GetComponentInChildren<Animator>();
        if (animator == null) animator = GetComponent<Animator>();
        if (attack != null) currentAttackCooldown = attack.GetCooldown();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Search for player if target is null
        if (target == null) {
            GameObject possibleTarget = GameObject.FindGameObjectWithTag(targetTag);
            if (possibleTarget != null)
            {
                target = possibleTarget;
                targetTransform = target.GetComponent<Transform>();
            }
        }
        PerformAttack();
    }

    void FixedUpdate()
    {
        currentKinematicTime -= Time.deltaTime;
        if (kinematic && currentKinematicTime <= 0) {
            rigidbody.isKinematic = true;
            kinematic = false;
            currentKinematicTime = kinematicCoolDown;
        } else if(!kinematic && currentKinematicTime <= 0) {
            rigidbody.isKinematic = false;
        }
        // update values
        if (currentAttackCooldown > 0f) currentAttackCooldown -= Time.deltaTime;
    }


    void PerformAttack()
    {
        if (Vector3.Distance(transform.position, targetTransform.position) >= attackRange) return;
        if (attack != null && currentAttackCooldown <= 0f)
        {
            attack.Attack(animator, target);
            currentAttackCooldown = attack.GetCooldown();
        }
    }

    public void Damage(float damage)
    {
        rigidbody.isKinematic = false;
        currentHealth -= damage;
        if (currentHealth == 0f) Kill();
        currentKinematicTime = kinematicCoolDown;
        kinematic = true;
        // rigidbody.isKinematic(true);
    }

    public void Kill()
    {
        if (deathAudio != null && oneshotAudioSource != null) oneshotAudioSource.PlayOneShot(deathAudio);
        animator.SetTrigger("kill");
        NavMovement nav = GetComponent<NavMovement>();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (nav != null) nav.Stop();
        if (rb != null) rb.velocity = Vector3.zero;
    }
}
