using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStateMachine : MonoBehaviour
{
    public ProjectileBaseState activeState;

    void Start()
    {

    }

    public void Initialise()
    {
        ChangeState(new ProjectilePatrolState());
    }
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(ProjectileBaseState newState)
    {
        if (activeState != null)
        {
            activeState.Exit();
        }
        activeState = newState;
        if (activeState != null)
        {
            activeState.proStateMachine = this;
            activeState.enemy = GetComponent<ProjectileEnemy>();
            activeState.Enter();
        }
    }
}

