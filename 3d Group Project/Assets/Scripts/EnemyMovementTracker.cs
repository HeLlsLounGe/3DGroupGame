using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementTracker : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent; 

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 agentVelocity = new Vector3 (agent.velocity.x, agent.velocity.y, agent.velocity.z);
        if (agentVelocity.magnitude > 0.5f)
        {
            animator.SetBool("IsMoving", true);
        }

        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}
