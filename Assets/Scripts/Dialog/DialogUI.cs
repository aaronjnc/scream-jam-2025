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

    private int selectedIndex = 0;
    
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
        dialogOptions.Clear();
        dialogOptions.AddRange(dialogObjects);
        GameObject horizontalObjPrefab = Instantiate(playerChoicePrefab, textObjParent);
        List<GameObject> newTextObjects = new List<GameObject>();
        foreach (DialogObject dialogObject in dialogObjects)
        {
            GameObject newTextObj = Instantiate(textObjPrefab, horizontalObjPrefab.transform);
            TMP_Text textComp = newTextObj.GetComponent<TMP_Text>();
            textComp.text = dialogObject.GetText();
            newTextObjects.Add(newTextObj);
        }
        selectedIndex = 0;
    }

    private void PrintBookLines(List<DialogObject> bookLines)
    {
        foreach (DialogObject bookLine in bookLines)
        {
            GameObject newTextObj = Instantiate(textObjPrefab, textObjParent);
            TMP_Text textComp = newTextObj.GetComponent<TMP_Text>();
            textComp.text = bookLine.GetText();
        }
        selectedDialog = bookLines[bookLines.Count - 1];
    }
}
