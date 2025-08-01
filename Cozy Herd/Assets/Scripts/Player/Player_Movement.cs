using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float sprintSpeedBonus = 5f;
    [SerializeField] private bool sprintButtonToggle = false;


    [Header("Slope Handling")]
    [SerializeField] private float slopeForce = 10f;
    [SerializeField] private float slopeRayLength = 1.5f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;

    //[Header("Animation")]
    //[SerializeField] private Animator animator;

    private Camera _playerCamera;
    private CharacterController _characterController;
    private Vector2 moveInput;
    private float verticalVelocity;

    // Sprint variables
    private bool isSprinting = false;
    private float currentSpeed;

    private static readonly int SpeedHash = Animator.StringToHash("Speed");

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerCamera = FindAnyObjectByType<Camera>();

        currentSpeed = movementSpeed;
    }

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        HandleMovement();
        HandleGravity();

        //UpdateAnimations();
    }

    //private void UpdateAnimations()
    //{
    //    if (animator == null) return;

    //    // Calculate current movement speed (horizontal)
    //    Vector3 horizontalVelocity = new Vector3(_characterController.velocity.x, 0, _characterController.velocity.z);
    //    float currentMovementSpeed = horizontalVelocity.magnitude;

    //    animator.SetFloat(SpeedHash, currentMovementSpeed);
    //}

    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            // Small downwards force to keep grounded
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            // Apply additional slope force if moving on a slope
            if (IsOnSlope() && moveInput != Vector2.zero)
            {
                verticalVelocity = -slopeForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // Apply gravity when not grounded
        }
    }

    private bool IsOnSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, slopeRayLength))
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            return angle > 0.1f && angle < _characterController.slopeLimit;
        }

        return false;
    }

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;

        // Handle horizontal movement
        if (moveInput != Vector2.zero)
        {
            // Get camera's forward and right directions (ignore the Y component)
            Vector3 cameraForward = _playerCamera.transform.forward;
            Vector3 cameraRight = _playerCamera.transform.right;

            // Flatten on Y axis for ground movement
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            // Calculate movement direction relative to camera
            Vector3 moveDirection = (cameraForward * moveInput.y) + (cameraRight * moveInput.x);
            movement = moveDirection * currentSpeed;
        }

        // Add vertical movement (gravity/falling)
        movement.y = verticalVelocity;

        // Apply the movement
        _characterController.Move(movement * Time.deltaTime);
    }

    public void HandleSprinting()
    {
        isSprinting = !isSprinting;

        if (isSprinting)
        {
            currentSpeed = movementSpeed + sprintSpeedBonus;
        }
        else
        {
            currentSpeed = movementSpeed;
        }
    }
}
