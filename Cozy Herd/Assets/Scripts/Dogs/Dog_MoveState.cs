using System.Xml.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Dog_MoveState : IState
{
    private Dog_StateMachine _stateMachine;

    public Dog_MoveState(Dog_StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        if (_stateMachine.NavMeshAgent != null && _stateMachine.NavMeshAgent.enabled && _stateMachine.NavMeshAgent.isOnNavMesh)
        {
            _stateMachine.NavMeshAgent.SetDestination(_stateMachine.TargetPosition);
        }
        else
        {
            Debug.LogWarning($"{_stateMachine.name}: Cannot set destination - agent not on NavMesh");
        }
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        //if(_stateMachine.NavMeshAgent.remainingDistance <= _stateMachine.stopDistance)
        //{
        //    _stateMachine.ChangeState(_stateMachine.IdleState);
        //}
        //else
        //{
        //    Vector3 direction = (_stateMachine.TargetPosition - _stateMachine.transform.position).normalized;
        //    Quaternion lookRotation = Quaternion.LookRotation(direction);
        //    _stateMachine.transform.rotation = Quaternion.RotateTowards(_stateMachine.transform.rotation, lookRotation, _stateMachine.rotationSpeed * Time.deltaTime);
        //}
    }
}
