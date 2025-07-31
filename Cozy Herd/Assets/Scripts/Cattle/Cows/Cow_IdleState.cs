using System;
using UnityEngine;

public class Cow_IdleState : IState
{
    private Cow_StateMachine _stateMachine;
    private Transform _player;

    public Cow_IdleState(Cow_StateMachine stateMachine)
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
        if (CheckIfPlayerClose())
        {
            _stateMachine.ChangeState(_stateMachine.RunningState);
        }
    }

    public bool CheckIfPlayerClose()
    {
        if (_player == null) return false;
        Vector3 directionToPlayer = (_player.position - _stateMachine.transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(_stateMachine.transform.position, _player.position);

        return distanceToPlayer <= _stateMachine.DetectionRange;
    }
}
