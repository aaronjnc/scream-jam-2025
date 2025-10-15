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

    private List<GameObject> textObjects = new List<GameObject>();

    private List<DialogObject> dialogOptions = new List<DialogObject>();

    private List<TMP_Text> playerOptions = new List<TMP_Text>();

    public void LoadPage(List<DialogObject> dialogObjects)
    {
        ClearText();
        LoadDialog(dialogObjects);
    }
    
    public void LoadDialog(List<DialogObject> dialogObjects)
    {
        if (dialogObjects[0].isPlayerOption())
        {
            PrintPlayerOptions(dialogObjects);
        }
        else
        {
            PrintBookLines(dialogObjects);
        }
    }

    private void PrintPlayerOptions(List<DialogObject> dialogObjects)
    {
        playerOptions.Clear();
        dialogOptions.Clear();
        dialogOptions.AddRange(dialogObjects);
        GameObject horizontalObjPrefab = Instantiate(playerChoicePrefab, textObjParent);
        foreach (DialogObject dialogObject in dialogObjects)
        {
            TMP_Text textComp = Instantiate(textObjPrefab, horizontalObjPrefab.transform).GetComponent<TMP_Text>();
            textComp.text = dialogObject.GetText();
            playerOptions.Add(textComp);
            textObjects.Add(textComp.gameObject);
        }
        textObjects.Add(horizontalObjPrefab);
        if (playerOptions.Count > 1)
        {
            playerOptions[0].color = Color.red;
        }
    }

    private void PrintBookLines(List<DialogObject> bookLines)
    {
        playerOptions.Clear();
        dialogOptions.Clear();
        foreach (DialogObject bookLine in bookLines)
        {
            GameObject newTextObj = Instantiate(textObjPrefab, textObjParent);
            TMP_Text textComp = newTextObj.GetComponent<TMP_Text>();
            textComp.text = bookLine.GetText();
            textObjects.Add(newTextObj);
        }
        selectedDialog = bookLines[bookLines.Count - 1];
    }

    private void ClearText()
    {
        for (int i = 0; i < textObjects.Count; i++)
        {
            Destroy(textObjects[i]);
            textObjects.RemoveAt(i);
            i--;
        }
    }

    public void UpdateChoice(int oldChoice, int newChoice)
    {
        playerOptions[oldChoice].color = Color.white;
        playerOptions[newChoice].color = Color.red;
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
}
