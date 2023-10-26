using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 100.0f;
    Vector2 movementInput;
    Rigidbody rigidbody;
    public InputActionAsset devControls;

    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        devControls.Enable();
        devControls.FindActionMap("Runner dev").FindAction("Move").performed += ctx => OnMove(ctx.ReadValue<Vector2>());
        devControls.FindActionMap("Runner dev").FindAction("Move").canceled += ctx => OnMove(ctx.ReadValue<Vector2>());
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(movementInput.y, 0.0f, 0.0f);
        moveDirection.Normalize();

        if (moveDirection != Vector3.zero)
        {
            // Calculate the rotation based on A and D keys
            Quaternion rotation = Quaternion.Euler(0, rotationSpeed * Time.fixedDeltaTime * movementInput.x, 0);

            // Apply the rotation using Rigidbody
            rigidbody.MoveRotation(rigidbody.rotation * rotation);

            // Calculate the velocity and apply it using Rigidbody
            Vector3 velocity = transform.right * moveSpeed * -movementInput.y;
            rigidbody.velocity = velocity;
        }
    }

    public void OnMove(Vector2 value)
    {
        // Called when WASD keys are pressed
        movementInput = value;
    }
}
