using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour
{
    [SerializeField] Transform battingPoint;
    [SerializeField] float attackRange, attackRate, batRange, batDelay;
    [SerializeField] int damage;
    float attackTimer;
    GameObject player;
    private NavMeshAgent agent;
    [SerializeField] LayerMask playerlayer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        attackTimer = attackRate;
    }

    
    void Update()
    {

        Vector3 lookPoint = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        agent.destination = player.transform.position;
        transform.LookAt(lookPoint);

        if (attackTimer > 0)
        {
            attackTimer = attackTimer - Time.deltaTime;
        }

        if ((transform.position - lookPoint).sqrMagnitude < attackRange && attackTimer <= 0)
        {
            Invoke("Swing", batDelay);
            attackTimer = attackRate;
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (battingPoint == null) { return; }
        Gizmos.DrawWireSphere(battingPoint.position, batRange);
    }

    void Swing()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(battingPoint.position, batRange, playerlayer);

        foreach (Collider player in hitPlayer)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
