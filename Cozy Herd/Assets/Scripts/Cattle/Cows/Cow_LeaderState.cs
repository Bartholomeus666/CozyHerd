using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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
