using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBotController : MonoBehaviour, IDamageable<float>, IKillable
{
    private float health = 2f;
    private float attackRange = 50f;
    private float shotAngleHorizontal = 5f;
    private float shotAngleVertical = 90f;

    private string targetTag = "Player";

    float currentHealth;
    float currentAttackCooldown = 0f;
    float attackCooldown = 2f;
    float shotSpeed = 14;

    Transform targetTransform;
    Animator animator;
    IAttack<Animator, GameObject> attack;

    public bool testKill = false;
    public bool testDamage = false;
    public GameObject enemyAttackSphere;
    public Transform rightGunArm;
    public Transform leftGunArm;
    public LayerMask layerMask;
    public AudioSource attackAudio;
    public AudioClip deathSound;
    private Rigidbody rigidbody;
    public AudioSource walkSource; //We just want this to play the death sound, late change, gross.

    void Start()
    {
        currentHealth = health;
        attack = GetComponent<IAttack<Animator, GameObject>>();
        animator = GetComponentInChildren<Animator>();
        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayPlayerTarget = targetTransform.position;
        // Cooldown check
        if (currentHealth > 0) {
            if (currentAttackCooldown <= 0)
            {
                // Range check
                if (Vector3.Distance(rayOrigin, rayPlayerTarget) <= attackRange)
                {    
                    Vector3 toPosition = (rayPlayerTarget - transform.position);
                    float vertAngle = Vector3.SignedAngle(toPosition, transform.forward, Vector3.up);
                    //Are they within our verticle shot angle? (within 60 degrees)
                    if (Mathf.Abs(vertAngle) < shotAngleVertical)
                    {
                        toPosition.y = 0;
                        //Are they within out horizontal shot angle? (within 5 degrees)
                        float angleToPosition = Vector3.Angle(transform.forward, toPosition);
                        if (Mathf.Abs(angleToPosition) < shotAngleHorizontal) {

                            //Can we make the shot?
                            if (Physics.Raycast(rayOrigin, rayPlayerTarget - rayOrigin, out hit, 50f, layerMask))
                            {
                                //Make sure we aren't shooting at a wall
                                if (hit.transform.tag == "Player")
                                {
                                    PerformAttack();
                                    Debug.DrawRay(rayOrigin, rayPlayerTarget - rayOrigin, Color.grey, 10f);
                                }
                            } 
                        }
                    }
                }
            }
        }
        Debug.DrawRay(rayOrigin, rayPlayerTarget - rayOrigin, Color.cyan);
    }

    void FixedUpdate()
    {
        if (currentAttackCooldown > 0f) currentAttackCooldown -= Time.deltaTime;
    }

    void PerformAttack()
    {
        attackAudio.Play();
        animator.SetTrigger("Attack");
        Vector3 _direction = (targetTransform.position - transform.position).normalized;
        GameObject enemyattacksphere1 = Instantiate(enemyAttackSphere, leftGunArm.position, Quaternion.LookRotation(_direction)) as GameObject;
        enemyattacksphere1.GetComponent<Rigidbody>().velocity = enemyattacksphere1.transform.forward * shotSpeed;

        GameObject enemyattacksphere2 = Instantiate(enemyAttackSphere, rightGunArm.position, Quaternion.LookRotation(_direction)) as GameObject;
        enemyattacksphere2.GetComponent<Rigidbody>().velocity = enemyattacksphere2.transform.forward * shotSpeed;

        currentAttackCooldown = attackCooldown;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            rigidbody.isKinematic = true;
            Kill();
        }
        else
        {
            rigidbody.isKinematic = false;
            StartCoroutine(AfterShot());
        }
        
    }

    public void Kill()
    {
        animator.SetTrigger("Death");
        walkSource.Stop();
        walkSource.PlayOneShot(deathSound);
        AttackBotNavMovement nav = GetComponent<AttackBotNavMovement>();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (nav != null) nav.Stop();
        if (rb != null) rb.velocity = Vector3.zero;
    }

    private IEnumerator AfterShot()
    {
        yield return new WaitForSeconds(0.25f);
        rigidbody.isKinematic = true;
    }
}
