using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private string MoveInputAxis = "Horizontal";
    public float moveRate = 5;
    public float jumpSpeed = 4.0f;
    public float Gravity = 20;
    private bool jumpAllowed = true;
    private Rigidbody rb;
    private bool grounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveAxis = Input.GetAxis(MoveInputAxis);
        Physics.gravity = new Vector3(0, -Gravity, 0);
       
        ApplyInput(moveAxis);
    }
    private void ApplyInput(float moveInput)
    {
        Move(moveInput);
        
    }
    private void Move(float input)
    {
        rb.velocity = transform.right * input * moveRate;
        //rb.AddForce(transform.forward * input * moveRate, ForceMode.Force);
        if (Input.GetButtonDown("Jump") && jumpAllowed)
        {
            jumpAllowed = false;
            rb.velocity += jumpSpeed * Vector3.up;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        jumpAllowed = true;
    }
}
