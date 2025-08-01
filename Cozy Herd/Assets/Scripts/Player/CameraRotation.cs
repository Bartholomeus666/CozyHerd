using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 2f;

    private bool isRotating = false;
    private Vector2 mouseDelta;

    public void OnMouseRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRotating = true;
            Debug.Log("context performed");
        }
        else if (context.canceled)
        {
            isRotating = false;
            Debug.Log("context canceled");
        }
    }

    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
        Debug.Log($"Mouse Delta: {mouseDelta}");
    }

    private void Update()
    {
        if (isRotating)
        {
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        Debug.Log($"Direct Mouse Delta: {mouseDelta}");

        transform.Rotate(Vector3.up, mouseDelta.x * Time.deltaTime * rotationSpeed);
    }
}
