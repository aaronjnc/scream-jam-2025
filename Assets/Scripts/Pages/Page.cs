using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page", menuName = "Scriptable Objects/Page")]
public class Page : ScriptableObject
{

    [SerializeField]
    private List<DialogObject> startDialog = new List<DialogObject>();

    [SerializeField]
    private Sprite pageImage;

    public List<DialogObject> GetStartDialog()
    {
        return startDialog;
    }

    public Sprite GetPageImage()
    {
        return pageImage;
    }
}
