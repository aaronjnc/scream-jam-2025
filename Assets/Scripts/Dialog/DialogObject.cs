using UnityEngine;

[CreateAssetMenu(fileName = "DialogObject", menuName = "Scriptable Objects/DialogObject")]
public class DialogObject : ScriptableObject
{
    [SerializeField]
    private string dialogKey;

    [SerializeField]
    private bool bPlayerOption;

    [SerializeField]
    private string text;

    [SerializeField]
    private string nextKey;

    [SerializeField]
    private string nextPage;

    public string GetDialogKey()
    {
        return dialogKey;
    }

    public bool isPlayerOption()
    {
        return bPlayerOption;
    }

    public string GetText()
    {
        return text;
    }
}
