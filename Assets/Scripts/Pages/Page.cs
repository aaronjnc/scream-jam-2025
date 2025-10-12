using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page", menuName = "Scriptable Objects/Page")]
public class Page : ScriptableObject
{
    [SerializeField]
    private string pageName;

    [SerializeField]
    private string startingKey;

    [SerializeField]
    private List<DialogObject> dialogList = new List<DialogObject>();

    public List<DialogObject> GetDialogList()
    {
        return dialogList;
    }

    public string GetFirstKey()
    {
        return startingKey;
    }

    public string GetPageName()
    {
        return pageName;
    }
}
