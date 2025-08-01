using UnityEngine;

public class Cow_LeaderState : IState
{
    private Cow_StateMachine _stateMachine;

    private Transform _player;

    public Cow_LeaderState(Cow_StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _stateMachine.NavMeshAgent.speed = 3.5f;
        Debug.Log("Leader state entered");
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        MoveayFromPlayer();
        FindNewHerdMembers();
    }

    public void MoveayFromPlayer()
    {
        Vector3 directionToPlayer = -((_player.position - _stateMachine.transform.position).normalized);

        if (_stateMachine.NavMeshAgent != null && _stateMachine.NavMeshAgent.enabled && _stateMachine.NavMeshAgent.isOnNavMesh)
        {
            _stateMachine.NavMeshAgent.SetDestination(_stateMachine.transform.position + directionToPlayer * _stateMachine.NavMeshAgent.speed);
        }
        else
        {
            Debug.LogWarning($"{_stateMachine.name}: Cannot set destination - agent not on NavMesh");
        }

        if (_stateMachine.isLeader)
        {
            //logic of lasso should be here
            if (Vector3.Distance(_stateMachine.transform.position, _player.position) < 2f)
            {
                _stateMachine.ChangeState(_stateMachine.LeaderState);
            }
        }
    }

    public void FindNewHerdMembers()
    {
        Collider[] colliders = Physics.OverlapSphere(_stateMachine.transform.position, _stateMachine.HerdRange);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Cow"))
            {
                Cow_StateMachine stateMachine = collider.gameObject.GetComponent<Cow_StateMachine>();
                if(stateMachine == null)
                {
                    continue;

                }
                if (!stateMachine.isFollowing && !stateMachine.isLeader)
                {
                    stateMachine.PickedUpByHerd(_stateMachine.Herd);
                }
            }
        }
    }
}
