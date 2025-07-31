using UnityEngine;

public class Dog_MoveState : IState
{
    private Dog_StateMachine _stateMachine;

    public Dog_MoveState(Dog_StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _stateMachine.NavMeshAgent.SetDestination(_stateMachine.TargetPosition);
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
