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
        if (_stateMachine.NavMeshAgent != null && _stateMachine.NavMeshAgent.enabled && _stateMachine.NavMeshAgent.isOnNavMesh)
        {
            _stateMachine.NavMeshAgent.SetDestination(_stateMachine.Herd.HerdLeader.transform.position);
        }
        else
        {
            Debug.LogWarning($"{_stateMachine.name}: Cannot set destination - agent not on NavMesh");
        }
    }
}
