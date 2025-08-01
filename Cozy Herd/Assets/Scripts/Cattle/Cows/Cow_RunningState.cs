using UnityEngine;

public class Cow_RunningState : IState
{
    private Transform _player;

    private Cow_StateMachine _stateMachine;

    public Cow_RunningState(Cow_StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        Vector3 directionToPlayer = -((_player.position - _stateMachine.transform.position).normalized);

        _stateMachine.NavMeshAgent.SetDestination(_stateMachine.transform.position + directionToPlayer * _stateMachine.NavMeshAgent.speed);

        if (_stateMachine.isLeader)
        {
            //logic of lasso should be here
            if (Vector3.Distance(_stateMachine.transform.position, _player.position) < 2f)
            {
                _stateMachine.ChangeState(_stateMachine.LeaderState);
            }
        }
    }
}
