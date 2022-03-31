using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Hacky solution to fix root movement issues on animators tied to NavMeshAgents
 */
public class FixRootMovementOnNavAgent : MonoBehaviour
{

    public float rotateSpeed = 10f;

    Animator anim;
    NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent == null) agent = GetComponentInChildren<NavMeshAgent>();
    }
    void Update()
    {
        //direction towards current "corner" on the current path towards destination
        Vector3 targetDirection = agent.steeringTarget - transform.position;
        targetDirection.y = 0; //Set y to zero so that the agent doesn't rotate upwards/downwards

        //rotate smoothly towards desired direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), rotateSpeed * Time.deltaTime);
    }

    void OnAnimatorMove()
    {
        Vector3 animVelocity = anim.deltaPosition / Time.deltaTime;
        agent.velocity = animVelocity;
    }
}
