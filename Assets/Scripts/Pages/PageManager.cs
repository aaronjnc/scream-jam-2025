using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField]
    private List<Page> pages = new List<Page>();

    [SerializeField]
    private Page currentPage;

    [SerializeField]
    private string startPage;

    private Dictionary<string, Page> pageDictionary = new Dictionary<string, Page>();

    private void Awake()
    {
        foreach (Page page in pages)
        {
            pageDictionary.Add(page.GetPageName(), page);
        }
        NextPage(startPage);
    }

    public Page GetCurrentPage()
    {
        return currentPage;
    }

    public void NextPage(string nextPage)
    {
        currentPage = pageDictionary[nextPage];
    }
}
