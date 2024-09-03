using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 0f;

    Vector3 movement;
    Quaternion rotation = Quaternion.identity;

    Rigidbody rb;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        bool hasInputHorizontal = !Mathf.Approximately(horizontal, 0f);
        bool hasInputVerizontal = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasInputHorizontal || hasInputVerizontal;
        if (isWalking)
        {
            animator.SetBool("IsWalking", true);
        } else if (!isWalking)
        {
            animator.SetBool("IsWalking", false);
        }
        

        Vector3 desiredFoward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation(desiredFoward);
    }

    private void OnAnimatorMove()
    {
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        rb.MoveRotation(rotation);
    }
}
