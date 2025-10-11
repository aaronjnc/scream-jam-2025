using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private PageManager pageManager;

    [SerializeField]
    private DialogUI dialogUI;

    private Dictionary<string, List<DialogObject>> pageDialog = new Dictionary<string, List<DialogObject>>();

    private void Awake()
    {
        
    }

    private void LoadPage()
    {
        Page currentPage = pageManager.GetCurrentPage();
        List<DialogObject> dialogObjects = currentPage.GetDialogList();
        foreach (DialogObject dialog in dialogObjects)
        {
            if (!pageDialog.ContainsKey(dialog.GetDialogKey()))
            {
                pageDialog.Add(dialog.GetDialogKey(), new List<DialogObject>());
            }
            pageDialog[dialog.GetDialogKey()].Add(dialog);
        }
        dialogUI.LoadDialog(pageDialog[currentPage.GetFirstKey()]);
    }
}
