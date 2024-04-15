using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    

   
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        Vector3 Lookpoint = new Vector3(enemy.Player.transform.position.x, enemy.transform.position.y, enemy.Player.transform.position.z);
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(Lookpoint);


            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //Change to search state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    public void Shoot()
    {
        enemy.Fire();
        shotTimer = 0;
        //Transform gunbarrel = enemy.gunBarrel;
        //GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        //Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //bullet.GetComponent<Rigidbody>().velocity = shootDirection * 40;
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
