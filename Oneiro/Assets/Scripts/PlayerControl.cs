using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerControl : MonoBehaviour
{
    #region Variables
    [Header("Movement:")]
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

    [Header("Held objects:")]
    public float grabDistance;
    private GameObject heldObject;
    #endregion

    void Awake()
    {
        //Initialise local components
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveAxis = Input.GetAxis("Left_Joystick_Horizontal");

        Move(moveAxis);

        if (Input.GetAxis("Triggers") > 0 && heldObject == null)
        {
            //Check for push/pull objects in front of player
            print("Pulled right trigger.");
            PushPull();
        }
        else if (heldObject != null && Input.GetAxis("Triggers") <= 0)
        {
            //Release object
            heldObject.transform.parent = null;
            heldObject = null;
        }
    }

    private void Move(float input)
    {
        if (input > 0)
        {
            //Walk toward right of screen using root motion
            transform.rotation = Quaternion.Euler(forwardRotation);
            anim.SetBool("Walking", true);
        }
        else if (input < 0)
        {
            //Walk toward left of screen using root motion
            transform.rotation = Quaternion.Euler(backwardRotation);
            anim.SetBool("Walking", true);
        }
        else if (anim.GetBool("Walking") == true)
        {
            //Stop walking
            anim.SetBool("Walking", false);
        }

        //Jump
        if (Input.GetButtonDown("A-Button") && jumpAllowed)
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

    void PushPull()
    {
        //Check for objects in front of player
        if (Physics.Raycast(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward), out RaycastHit hit, grabDistance))
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.transform.tag == "PushPull")
            {
                //Grab the object if it is a push/pull object
                heldObject = hit.transform.gameObject;
                heldObject.transform.parent = transform;
            }
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * grabDistance, Color.white);
        }
    }
}
