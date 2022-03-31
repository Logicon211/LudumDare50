using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class guncontroller : MonoBehaviour { 

    public float reloadSpeed = 0.5f;
    private float reloadCooldown = 0f;
    private int gunDamage = 1;
    public float hitForce = 1000f;


    private Animator gunAnimator;
    private CharacterController FPcontrollerScript;
    private AudioSource gunSound;
    private LineRenderer gunshottrail;
    public Transform bulletSpawnPoint;
    private Camera playerCamera;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    public LayerMask layermask;
    public GameObject enemyHit;
    public GameObject otherHit;
    public AudioClip nobabies;


    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        FPcontrollerScript = GetComponentInParent<CharacterController>();
        gunSound = GetComponent<AudioSource>();
        gunshottrail = GetComponent<LineRenderer>();
        playerCamera = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadCooldown <= 0f && CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
            RaycastHit hit;

            gunshottrail.SetPosition(0, bulletSpawnPoint.position);
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, 200f, layermask))
            {
                // We don't shoot babies
                if (hit.transform.tag != "Baby")
                {
                    StartCoroutine(ShotEffect());
                    gunshottrail.SetPosition(1, hit.point);

                    Shootable health = hit.collider.GetComponent<Shootable>();
                    if (health != null)
                    {
                        health.Damage();
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                    }
                    Instantiate(enemyHit, hit.point, Quaternion.identity);
                }
                else
                {
                    gunSound.PlayOneShot(nobabies);
                    reloadCooldown = reloadSpeed;
                }
            }
            else
            {
                gunshottrail.SetPosition(1, rayOrigin + playerCamera.transform.forward * 200);
                StartCoroutine(ShotEffect());
            }


        }
            gunAnimator.SetBool("isWalking", (FPcontrollerScript.velocity.magnitude > 0 && FPcontrollerScript.isGrounded));
    }

    void FixedUpdate()
    {
        if(reloadCooldown > 0f)
        {
            reloadCooldown = reloadCooldown - Time.fixedDeltaTime;
            
        }
    }
 

    private IEnumerator ShotEffect()
    {
        reloadCooldown = reloadSpeed;
        gunAnimator.SetTrigger("Shoot");
        gunSound.Play();
        gunshottrail.enabled = true;
        yield return shotDuration;
        gunshottrail.enabled = false;
    }

}
