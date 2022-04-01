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
    
    private float playerspeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidBody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
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
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);
        }
    }

    void FixedUpdate()
    {
        //Get Movement unputs
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
    }
}
