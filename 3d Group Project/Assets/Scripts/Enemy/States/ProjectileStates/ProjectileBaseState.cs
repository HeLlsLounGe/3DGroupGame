public abstract class ProjectileBaseState
{
    public ProjectileEnemy enemy;

    public ProjectileStateMachine proStateMachine;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}
