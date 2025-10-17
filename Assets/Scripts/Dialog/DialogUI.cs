using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [SerializeField]
    private GameObject textObjPrefab;

    [SerializeField]
    private Transform textObjParent;

    [SerializeField]
    private GameObject playerChoicePrefab;

    private DialogObject selectedDialog;

    private AudioSource audioSource;

    private List<GameObject> textGameObjects = new List<GameObject>();

    private List<DialogObject> dialogOptions = new List<DialogObject>();

    private List<DialogObjectUI> dialogOptionsUI = new List<DialogObjectUI>();

    private List<DialogObjectUI> dialogUI = new List<DialogObjectUI>();

    private bool bIsImpaired = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadPage(List<DialogObject> dialogObjects, bool updateImpaired)
    {
        SkipTexts();
        ClearText();
        LoadDialog(dialogObjects, updateImpaired);
    }
    
    public void LoadDialog(List<DialogObject> dialogObjects, bool updateImpaired)
    {
        SkipTexts();
        dialogOptions.Clear();
        dialogOptionsUI.Clear();
        if (!bIsImpaired)
        {
            bIsImpaired = updateImpaired;
        }
        if (dialogObjects.Count == 0)
        {
            return;
        }
        if (dialogObjects[0].isPlayerOption())
        {
            PrintPlayerOptions(dialogObjects);
        }
        else
        {
            PrintBookLines(dialogObjects);
        }
    }

    public void SkipTexts()
    {
        foreach (DialogObjectUI dialogObject in dialogUI)
        {
            dialogObject.Skip();
        }
    }

    private void PrintPlayerOptions(List<DialogObject> dialogObjects)
    {
        dialogOptions.AddRange(dialogObjects);
        GameObject horizontalObjPrefab = Instantiate(playerChoicePrefab, textObjParent);
        foreach (DialogObject dialogObject in dialogObjects)
        {
            DialogObjectUI textComp = Instantiate(textObjPrefab, horizontalObjPrefab.transform).GetComponent<DialogObjectUI>();
            textComp.SetDialogObject(this, dialogObject, bIsImpaired);
            dialogOptions.Add(dialogObject);
            dialogOptionsUI.Add(textComp);
            dialogUI.Add(textComp);
            textGameObjects.Add(textComp.gameObject);
        }
        textGameObjects.Add(horizontalObjPrefab);
        if (dialogOptionsUI.Count > 1)
        {
            dialogOptionsUI[0].Select();
        }
    }

    private void PrintBookLines(List<DialogObject> bookLines)
    {
        foreach (DialogObject bookLine in bookLines)
        {
            GameObject newTextObj = Instantiate(textObjPrefab, textObjParent);
            DialogObjectUI textComp = newTextObj.GetComponent<DialogObjectUI>();
            textComp.SetDialogObject(this, bookLine, bIsImpaired);
            dialogUI.Add(textComp);
            textGameObjects.Add(newTextObj);
        }
        selectedDialog = bookLines[bookLines.Count - 1];
    }

    private void ClearText()
    {
        dialogUI.Clear();
        dialogOptions.Clear();
        dialogOptionsUI.Clear();
        for (int i = 0; i < textGameObjects.Count; i++)
        {
            Destroy(textGameObjects[i]);
            textGameObjects.RemoveAt(i);
            i--;
        }
    }

    public void UpdateChoice(int oldChoice, int newChoice)
    {
        dialogOptionsUI[oldChoice].Deselect();
        dialogOptionsUI[newChoice].Select();
    }

    public DialogObject GetDialogChoice(int choice)
    {
        if (dialogOptions.Count == 0)
        {
            return selectedDialog;
        }
        return dialogOptions[choice];
    }

    public int GetOptionCount()
    {
        return dialogOptions.Count;
    }

    public void PlayAudioClip(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
