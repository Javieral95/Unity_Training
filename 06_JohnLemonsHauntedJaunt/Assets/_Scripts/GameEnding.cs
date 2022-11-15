using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    [SerializeField, Range(0, 5)]
    private float fadeDuration = 1f;

    [SerializeField, Range(0, 5)]
    private float displayImageDuration = 2f;

    private bool isPlayerAtExit;
    private bool isPlayerCaught;

    private float timer;
    //Quiza mejor etiqueta etc, pero por hacer un ejemplo de este modo
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    public AudioSource exitAudio, caughtAudio;
    private bool hasAudioPlayed;

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, caughtAudio, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }

    }

    private void EndLevel(CanvasGroup canvasGroup, AudioSource audioSource, bool doRestart = false)
    {
        timer += Time.deltaTime;
        canvasGroup.alpha = (timer / fadeDuration); // Se queda en 1

        if (timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Para reset
            else
            {
                Debug.Log("APPLICATION ENDED !!!");
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name) //Para reset
                Application.Quit(); //No funcionará en el editor
            }
        }

        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
    }

    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
}
