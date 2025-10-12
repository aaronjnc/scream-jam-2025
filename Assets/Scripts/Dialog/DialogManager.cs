using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private PageManager pageManager;

    [SerializeField]
    private DialogUI dialogUI;

    private Dictionary<string, List<DialogObject>> pageDialog = new Dictionary<string, List<DialogObject>>();

    private int selectedIndex = 0;

    private void Start()
    {
        LoadPage();
    }

    private void LoadPage()
    {
        selectedIndex = 0;
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

    public void SwitchChoice(CallbackContext ctx)
    {
        int dir = (int)ctx.ReadValue<float>();
        int oldChoice = selectedIndex;
        selectedIndex = Mathf.Clamp(selectedIndex + dir, 0, dialogUI.GetOptionCount());
    }

    public void NextDialog(CallbackContext ctx)
    {

    }
}
