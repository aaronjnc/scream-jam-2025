using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> sizeList = new List<Vector3>();

    [SerializeField]
    private List<Vector3> rotList = new List<Vector3>();

    private int listNum = 0;

    [SerializeField]
    private DialogManager dialogManager;

    public void StartClick()
    {
        if (listNum >= sizeList.Count)
        {
            dialogManager.StartGame();
            return;
        }
        transform.localScale = sizeList[listNum];
        transform.rotation = Quaternion.Euler(rotList[listNum]);
        listNum++;
    }
}
