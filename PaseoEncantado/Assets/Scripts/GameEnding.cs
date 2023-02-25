using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    bool m_isPlayerAtExit;
    bool m_isPlayerCaught;
    float m_Timer;
    public float displayImageDuration = 1f;
    public AudioSource exitSource;
    public AudioSource caughtSource;
    bool m_HasAudioPlayed = false;
    void Update()
    {
        if (m_isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitSource); 
        } 
        else if(m_isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtSource);
        }
    }

    public void CaughtPlayer()
    {
        m_isPlayerCaught = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            m_isPlayerAtExit = true;
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        Debug.Log("Se llama a EndLevel");
        if(!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
            
        }
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
