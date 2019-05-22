using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerControl : MonoBehaviour
{
    #region Variables
    //Movement variables
    public float moveRate;
    public float jumpSpeed;
    public float gravity;
    public bool jumpAllowed;
    private Vector3 velocity;

    //Rotation
    private Vector3 forwardRotation = new Vector3(0, 270, 0);
    private Vector3 backwardRotation = new Vector3(0, 90, 0);

    //Components
    private Rigidbody rb;
    private Animator anim;
    #endregion

    void Awake()
    {
        //Initialise local components
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        float moveAxis = Input.GetAxis("Horizontal");
        //Physics.gravity = new Vector3(0, -gravity, 0);
       
        Move(moveAxis);
    }

    private void Move(float input)
    {
        if (input > 0)
        {
            transform.rotation = Quaternion.Euler(forwardRotation);

            anim.SetBool("Walking", true);

            //velocity = rb.velocity;
            //velocity.z = 0;
            //velocity += transform.forward * input * moveRate;

            //rb.velocity = velocity;
        }
        else if (input < 0)
        {
            transform.rotation = Quaternion.Euler(backwardRotation);

            anim.SetBool("Walking", true);

            //velocity = rb.velocity;
            //velocity.z = 0;
            //velocity += -transform.forward * input * moveRate;

            //rb.velocity = velocity;
        }
        else if (anim.GetBool("Walking") == true)
            anim.SetBool("Walking", false);

        //Jump
        if (Input.GetButtonDown("Jump") && jumpAllowed)
        {
            print("Trying to jump.");
            jumpAllowed = false;
            rb.velocity += jumpSpeed * Vector3.up;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Reset jump
        jumpAllowed = true;
        print("Jump reset.");
    }
}
