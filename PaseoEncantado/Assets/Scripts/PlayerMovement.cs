using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;
    Vector2 inputMovement;
    Animator m_Animator;
    public float turnSpeed;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    private AudioSource m_AudioSource;
    void Start()
    {
        
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        m_Movement.Set(inputMovement.x, 0f, inputMovement.y);
        m_Movement.Normalize();
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool horizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool verticalInput = !Mathf.Approximately(vertical, 0f);

        bool itsWalking = horizontalInput || verticalInput;
        m_Animator.SetBool("itsWalking", itsWalking);
        if(itsWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }   
           
        } 
        else
        {
            m_AudioSource.Stop();
        }
     


        Vector3 desireForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        m_Rotation = Quaternion.LookRotation(desireForward);
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
   
}
