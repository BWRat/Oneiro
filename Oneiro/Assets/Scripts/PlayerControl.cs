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
    private Vector3 m_velocity;

    //Rotation
    private Vector3 m_forwardRotation = new Vector3(0, 270, 0);
    private Vector3 m_backwardRotation = new Vector3(0, 90, 0);

    //Components
    private Rigidbody m_rb;
    private Animator m_anim;

    [Header("Held objects:")]
    public float grabDistance;
    private GameObject m_heldObject;
    #endregion

    void Awake()
    {
        //Initialise local components
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float xMoveAxis = Input.GetAxis("Left_Joystick_Horizontal");
        float zMoveAxis = Input.GetAxis("Left_Joystick_Vertical");
        Vector3 movement = new Vector3(xMoveAxis, 0, zMoveAxis);

        Move(movement);

        if (Input.GetAxis("Right_Trigger") > 0 && m_heldObject == null)
        {
            //Check for push/pull objects in front of player
            PushPull();
        }
        else if (m_heldObject != null && Input.GetAxis("Right_Trigger") <= 0)
        {
            //Release object
            m_heldObject.transform.parent = null;
            m_heldObject = null;

            if (m_anim.GetBool("Grabbing") == true)
                m_anim.SetBool("Grabbing", false);
        }
    }

    private void Move(Vector3 input)
    {
        float anyInput = Mathf.Abs(input.x) + Mathf.Abs(input.z);

        if (anyInput > .25f)
        {
            //Walk toward right of screen using root motion
            //transform.rotation = Quaternion.Euler(m_forwardRotation);
            transform.forward = input;
            m_anim.SetBool("Walking", true);

            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -3, 3));

            //m_anim.speed = moveRate * input;
        }
        //else if (anyInput < -.25f)
        //{
        //    //Walk toward left of screen using root motion
        //    transform.rotation = Quaternion.Euler(m_backwardRotation);
        //    m_anim.SetBool("Walking", true);
        //    //m_anim.speed = Mathf.Abs(moveRate * input);
        //}
        else if (m_anim.GetBool("Walking") == true)
        {
            //Stop walking
            m_anim.SetBool("Walking", false);
            m_anim.speed = 1;
        }

        //Jump
        if (Input.GetButtonDown("A-Button") && jumpAllowed)
        {
            m_rb.velocity += jumpSpeed * Vector3.up;
            m_anim.SetTrigger("Jump");
            jumpAllowed = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Reset jump
        jumpAllowed = true;
    }

    void PushPull()
    {
        //Check for objects in front of player
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, grabDistance))
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if (hit.transform.tag == "PushPull")
            {
                if (m_heldObject == null)
                {
                    m_anim.SetTrigger("Grab");
                    m_anim.SetBool("Grabbing", true);
                }

                //Grab the object if it is a push/pull object
                m_heldObject = hit.transform.gameObject;
                m_heldObject.transform.parent = transform;
            }
            else if (m_anim.GetBool("Grabbing") == true)
                m_anim.SetBool("Grabbing", false);
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up, transform.TransformDirection(Vector3.forward) * grabDistance, Color.white);

            if (m_anim.GetBool("Grabbing") == true)
                m_anim.SetBool("Grabbing", false);
        }
    }
}
