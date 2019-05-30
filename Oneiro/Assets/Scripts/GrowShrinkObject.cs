using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GrowShrinkObject : MonoBehaviour
{
    #region Variables
    public bool startSmall;
    
    private Animator m_anim;
    #endregion

    void Start()
    {
        m_anim = GetComponent<Animator>();

        if (startSmall)
        {
            m_anim.SetTrigger("StartSmall");
            m_anim.SetBool("Grow", false);
            m_anim.SetBool("Shrink", true);
        }
        else
        {
            m_anim.SetBool("Grow", true);
            m_anim.SetBool("Shrink", false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("X-Button"))
            Toggle();
    }

    public void Toggle()
    {
        print("Toggling.");
        
        if (m_anim.GetBool("Grow") == true)
        {
            m_anim.SetBool("Grow", false);
            m_anim.SetBool("Shrink", true);
        }
        else
        {
            m_anim.SetBool("Grow", true);
            m_anim.SetBool("Shrink", false);
        }
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.GetComponent<PlayerControl>())
    //        Toggle();
    //}
}
