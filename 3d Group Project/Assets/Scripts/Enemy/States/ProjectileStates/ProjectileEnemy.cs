using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.InputSystem.XR;

public class ProjectileEnemy : MonoBehaviour
{
    private EnemyAttackScript enemyAttackScript;
    private ProjectileStateMachine proStateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    public Animator animator;
    // private GameObject self;
    public GameObject Player { get => player; }
    // public GameObject Self { get => self; }
    public NavMeshAgent Agent { get => agent; }
    [Header("Sight Values")]
    [SerializeField]
    public Path path;
    public float sightDistance = 20;
    public float fieldOfView = 180;
    public float eyeHeight;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10)]
    public float fireRateUpper;
    [Range(0.1f, 10)]
    public float fireRateLower;
    public float fireRate;
    private string currentState;
    void Awake()
    {
        proStateMachine = GetComponent<ProjectileStateMachine>();
        agent = GetComponent<NavMeshAgent>();
        proStateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        fireRate = Random.Range(fireRateUpper, fireRateLower);
    }


    void Update()
    {
        CanSeePlayer();
        currentState = proStateMachine.activeState.ToString();
    }



    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;

    }
}
