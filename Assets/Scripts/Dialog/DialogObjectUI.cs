using System.Collections;
using TMPro;
using UnityEngine;

public class DialogObjectUI : MonoBehaviour
{
    private DialogObject dialogObject;
    
    private DialogUI dialogUI;

    private TMP_Text textObj;

    private bool bIsImpaired = false;

    private Coroutine runningCoroutine;

    private void Awake()
    {
        textObj = GetComponent<TMP_Text>();
        textObj.enabled = false;
    }

    public void Skip()
    {
        if (runningCoroutine != null)
        {
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
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
        textObj.enabled = true;
    }

    public void PlayAudio()
    {
        AudioClip clip = dialogObject.GetAudioClip();
        switch (dialogObject.GetSoundTiming())
        {
            case DialogObject.ESoundTiming.Before:
                dialogUI.PlayAudioClip(clip);
                runningCoroutine = StartCoroutine(WaitForAudio(clip.length));
                break;
            case DialogObject.ESoundTiming.During:
                dialogUI.PlayAudioClip(clip);
                DisplayText();
                break;
            case DialogObject.ESoundTiming.After:
                DisplayText();
                dialogUI.PlayAudioClip(clip);
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
