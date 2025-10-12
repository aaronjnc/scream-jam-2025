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

    private List<DialogObject> dialogOptions = new List<DialogObject>();

    private List<TMP_Text> playerOptions = new List<TMP_Text>();
    
    public void LoadDialog(List<DialogObject> dialogObjects)
    {
        ClearText();
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
        }
    }

    private void PrintBookLines(List<DialogObject> bookLines)
    {
        playerOptions.Clear();
        foreach (DialogObject bookLine in bookLines)
        {
            GameObject newTextObj = Instantiate(textObjPrefab, textObjParent);
            TMP_Text textComp = newTextObj.GetComponent<TMP_Text>();
            textComp.text = bookLine.GetText();
        }
        selectedDialog = bookLines[bookLines.Count - 1];
    }

    private void ClearText()
    {
        Transform[] children = textObjParent.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
        {
            Destroy(children[i].gameObject);
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
        return dialogOptions[choice];
    }

    public int GetOptionCount()
    {
        return dialogOptions.Count;
    }
}
