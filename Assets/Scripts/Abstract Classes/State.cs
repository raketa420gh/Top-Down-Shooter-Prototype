public abstract class State
{
    protected NPC npc;
    protected StateMachine stateMachine;

    protected State(NPC npc, StateMachine stateMachine)
    {
        this.npc = npc;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}