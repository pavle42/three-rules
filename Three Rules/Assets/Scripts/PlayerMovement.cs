using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator animator;
    private float horizontalMovement;
    private float verticalMovement;
    private Rigidbody rigidbody;
    private Vector3 inputDirection;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");

        inputDirection = GetCameraRelativeInputDirection(horizontalMovement, verticalMovement);

        HandleAnimations();
    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + inputDirection * movementSpeed * Time.fixedDeltaTime);

        if (inputDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(inputDirection, Vector3.up);
        }
    }

    Vector3 GetCameraRelativeInputDirection(float horizontal, float vertical)
    {
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        return (cameraForward * verticalMovement + cameraRight * horizontalMovement).normalized;
    }

    void HandleAnimations()
    {
        animator.SetBool("isRunning", inputDirection != Vector3.zero);
    }
}
