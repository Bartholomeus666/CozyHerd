using System;
using UnityEngine;
using UnityEngine.AI;

public class Cow_StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    public NavMeshAgent NavMeshAgent;

    public bool isLeader = false;
    public bool isFollowing = false;

    public Cow_LeaderState LeaderState;
    public Cow_FollowingState FollowingState;
    public Cow_IdleState IdleState;
    public Cow_RunningState RunningState;

    public float DetectionRange = 10f;
    public float HerdRange = 5f;

    public Herd Herd = null;

    private void Start()
    {
        if(isLeader)
        {
            Herd = GetComponent<Herd>();
        }


        LeaderState = new Cow_LeaderState(this);
        FollowingState = new Cow_FollowingState(this);
        IdleState = new Cow_IdleState(this);
        RunningState = new Cow_RunningState(this);

        ChangeState(IdleState);
    }

    private void Update()
    {
        CurrentState.Update();
    }

    public void PickedUpByHerd(Herd herd)
    {
        Debug.Log($"Cow {gameObject.name} picked up by herd {herd.name}");
        isFollowing = true;
        Herd = herd;
        Herd.herdMembers.Add(this.gameObject);
        ChangeState(FollowingState);
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}
