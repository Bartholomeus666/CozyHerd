using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Dog_StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }

    public NavMeshAgent NavMeshAgent;

    public Dog_IdleState IdleState;
    

    [Header("Dog MoveState")]
    public Dog_MoveState MoveState;
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float stopDistance = 0.1f;
    public Vector3 TargetPosition;

    private void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();

        IdleState = new Dog_IdleState();
        MoveState = new Dog_MoveState(this);

        ChangeState(IdleState);
    }

    private void Update()
    {
        CurrentState.Update();
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    public void GetMoveOrder(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            TargetPosition = GetWorldPositionFromScreenPoint(mouseScreenPosition);
            
            
            ChangeState(MoveState);
        }
    }

    private Vector3 GetWorldPositionFromScreenPoint(Vector2 screenPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
