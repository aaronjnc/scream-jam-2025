using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField]
    private List<Page> pages = new List<Page>();

    [SerializeField]
    private Page currentPage;

    public Page GetCurrentPage()
    {
        return currentPage;
    }
}
