using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;
    Animator m_Animator;
    public float turnSpeed;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0, vertical);
        m_Movement.Normalize();

        bool horizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool verticalInput = !Mathf.Approximately(vertical, 0f);

        bool itsWalking = horizontalInput || verticalInput;
        m_Animator.SetBool("itsWalking", itsWalking);


        Vector3 desireForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        m_Rotation = Quaternion.LookRotation(desireForward);
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position = m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
