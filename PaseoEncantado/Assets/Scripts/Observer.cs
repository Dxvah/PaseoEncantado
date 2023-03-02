using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool m_IsplayerInRange;
    public GameEnding gameEnding;
    public AudioSource p_IsOnView;
    float c_Timer;
    public CanvasGroup exclamationImageBackgroundCanvaGroup;
    public float timeToReact = 2f;
    bool p_IsOnRange = false;
    bool c_AudioPlayed = false;
    void Start()
    {
        p_IsOnView = GetComponent<AudioSource>();
        
    }

    
    void Update()
    {
        if(m_IsplayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    PlayerCaught(exclamationImageBackgroundCanvaGroup, true, p_IsOnView);                                        
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == player)
        {
            m_IsplayerInRange = true;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsplayerInRange = false;
        }
    }
    void PlayerCaught (CanvasGroup imageCanvasGroup, bool p_IsOnRange, AudioSource p_IsOnView)
    {
        Debug.Log ("en vision");
        if(!c_AudioPlayed)
        {
            p_IsOnView.Play();
            c_AudioPlayed = true;
        }
        imageCanvasGroup.alpha = c_Timer / timeToReact;
        c_Timer += Time.deltaTime;
        if (c_Timer == timeToReact)
        {
            gameEnding.CaughtPlayer();
        }
        

    }
}
