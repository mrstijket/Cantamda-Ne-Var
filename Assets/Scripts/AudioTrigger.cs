using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float doorCloseTime = 3f;
    public float neighbourResponseTime = 5f;
    public float walkTime = 1f;
    public void PlayClipButton()
    {
        audioSource.PlayOneShot(clip);
    }
    public void EnableWalkSound(GameObject gameObject)
    {
        StartCoroutine(enableWalkProcess(gameObject));
    }
    IEnumerator enableWalkProcess(GameObject gameObject)
    {
        yield return new WaitForSeconds(walkTime);
        gameObject.SetActive(true);
    }
    public void EnableTheObject(GameObject gameObject)
    {
        StartCoroutine(enableObjectProcess(gameObject));
    }
    IEnumerator enableObjectProcess(GameObject gameObject)
    {
        yield return new WaitForSeconds(neighbourResponseTime);
        gameObject.SetActive(true);
    }
    public void DisableTheObject(GameObject gameObject)
    {
        StartCoroutine(disableObjectProcess(gameObject));
    }
    IEnumerator disableObjectProcess(GameObject gameObject)
    {
        yield return new WaitForSeconds(doorCloseTime);
        gameObject.SetActive(false);
    }
}
