using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{

    [SerializeField]
    private Page currentPage;

    [SerializeField]
    private Page startPage;

    private void Awake()
    {
        NextPage(startPage);
    }

    public Page GetCurrentPage()
    {
        return currentPage;
    }

    public void NextPage(Page nextPage)
    {
        currentPage = nextPage;
    }
}
