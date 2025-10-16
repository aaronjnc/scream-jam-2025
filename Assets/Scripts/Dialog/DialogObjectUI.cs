using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DialogObjectUI : MonoBehaviour
{
    private DialogObject dialogObject;
    
    private DialogUI dialogUI;

    private AudioSource audioSource;
    private TMP_Text textObj;

    private bool bIsImpaired = false;

    private Coroutine runningCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        textObj = GetComponent<TMP_Text>();
    }

    public void Skip()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        DisplayText();
    }

    public void SetDialogObject(DialogUI dialogUIRef, DialogObject newDialogObject, bool bImpaired)
    {
        dialogUI = dialogUIRef;
        dialogObject = newDialogObject;
        bIsImpaired = bImpaired;
        if (dialogObject.GetAudioClip() != null)
        {
            PlayAudio();
        }
        else
        {
            DisplayText();
        }
    }

    public void DisplayText()
    {
        textObj.text = dialogObject.GetText(bIsImpaired);
    }

    public void PlayAudio()
    {
        AudioClip clip = dialogObject.GetAudioClip();
        audioSource.clip = clip;
        switch (dialogObject.GetSoundTiming())
        {
            case DialogObject.ESoundTiming.Before:
                audioSource.Play();
                runningCoroutine = StartCoroutine(WaitForAudio(clip.length));
                break;
            case DialogObject.ESoundTiming.During:
                audioSource.Play();
                DisplayText();
                break;
            case DialogObject.ESoundTiming.After:
                DisplayText();
                audioSource.Play();
                break;
        }
    }

    IEnumerator WaitForAudio(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        DisplayText();
    }

    public void Select()
    {
        textObj.color = Color.red;
    }

    public void Deselect()
    {
        textObj.color = Color.white;
    }
}
