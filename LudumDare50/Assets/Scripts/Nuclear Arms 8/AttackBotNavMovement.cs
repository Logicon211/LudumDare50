using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackBotNavMovement : MonoBehaviour
{

    public string targetTag = "Player";
    public float speed = 4;

    bool stopped = false;

    public AudioSource movementAudio;
    public NavMeshAgent agent;
    public Transform playerTarget;
    public Animator animationController;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponentInChildren<Animator>();
        agent.speed = speed;
        playerTarget = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    // Update is called once per frame
    void Update()
    {

        // Updating target position
        if (!stopped)
        {
            UpdateAnimation();
            if (Vector3.Distance(transform.position, playerTarget.position) <= 50)
            {
                agent.speed = speed;
            }
            else
            {
                agent.speed = speed * 2;
            }
            agent.destination = playerTarget.position;
        }
    }


    void UpdateAnimation()
    {
        if (animationController)
        {
            if (agent.velocity.magnitude >= 1f)
            {
                animationController.SetBool("moving", true);
                if (!movementAudio.isPlaying) movementAudio.Play(0);
                /*
                if (agent.velocity.y > 1f)
                {
                    animationController.SetBool("TurningRight", true);
                    animationController.SetBool("TurningLeft", false);
                }
                else if (agent.velocity.y < -1f)
                {
                    animationController.SetBool("TurningLeft", true);
                    animationController.SetBool("TurningRight", false);
                }
                else
                {
                    animationController.SetBool("TurningRight", false);
                    animationController.SetBool("TurningLeft", false);
                }
                */
            }
            else
            {
                if (!movementAudio.isPlaying) movementAudio.Play(0);

                movementAudio.Stop();
                animationController.SetBool("moving", false);
                //animationController.SetBool("TurningRight", false);
                //animationController.SetBool("TurningLeft", false);
            }
        }

    }
    private void FixedUpdate()
    {
        if (!stopped)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                agent.updateRotation = false;
                // Determine which direction to rotate towards
                Vector3 targetDirection = playerTarget.position - transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = agent.angularSpeed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else
            {
                agent.updateRotation = true;
            }
        }

    }

    public void Stop()
    {
        agent.speed = 0;
        agent.destination = gameObject.transform.position;
        stopped = true;
        agent.updateRotation = false;

    }

    public bool IsMoving()
    {
        return agent.velocity.magnitude >= 1f;
    }

}
