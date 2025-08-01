using UnityEngine;

public class Cow_FollowingState : IState
{
    private Cow_StateMachine _stateMachine;

    public Cow_FollowingState(Cow_StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }


    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Update()
    {
        _stateMachine.NavMeshAgent.SetDestination(_stateMachine.Herd.HerdLeader.transform.position);
    }
}
