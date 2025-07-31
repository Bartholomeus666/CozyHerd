using System;
using UnityEngine;
using UnityEngine.AI;

public class Cow_StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    public NavMeshAgent NavMeshAgent;

    public bool isLeader = false;

    public Cow_LeaderState LeaderState;
    public Cow_FollowingState FollowingState;
    public Cow_IdleState IdleState;
    public Cow_RunningState RunningState;

    public float DetectionRange = 10f;

    public string HerdName;

    private void Start()
    {
        if(isLeader)
        {
            HerdName = gameObject.name;
        }


        LeaderState = new Cow_LeaderState ();
        FollowingState = new Cow_FollowingState();
        IdleState = new Cow_IdleState(this);
        RunningState = new Cow_RunningState(this);

        ChangeState(IdleState);
    }

    private void Update()
    {
        CurrentState.Update();

        if (FindHerd())
        {

        }
    }

    private bool FindHerd()
    {


        return false;
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}
