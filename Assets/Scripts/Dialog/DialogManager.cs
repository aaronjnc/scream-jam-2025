using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private PageManager pageManager;

    [SerializeField]
    private DialogUI dialogUI;

    [SerializeField]
    private GameObject dialogPanel;

    [SerializeField]
    private GameObject startPanel;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private AudioSource gameMusicSource;

    private Image dialogImage;

    private int selectedIndex = 0;

    private bool bImpaired = false;

    [SerializeField]
    private AudioClip gameMusic;

    [SerializeField]
    private AudioClip badMusic;

    private void Start()
    {
        dialogImage = dialogPanel.GetComponent<Image>();
        dialogPanel.SetActive(false);
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        dialogPanel.SetActive(true);
        LoadPage();
        playerController.StartGame();
    }

    private void LoadPage()
    {
        selectedIndex = 0;
        Page currentPage = pageManager.GetCurrentPage();
        if (currentPage.name.Equals("EnterTheBook"))
        {
            gameMusicSource.clip = gameMusic;
            gameMusicSource.Play();
        }
        else if (currentPage.name.Equals("BadEnding"))
        {
            gameMusicSource.clip = badMusic;
            gameMusicSource.Play();
        }
        else if (currentPage.name.Equals("GoodEnding"))
        {
            gameMusicSource.Stop();
        }
        if (currentPage.GetPageImage() != null)
        {
            dialogImage.sprite = currentPage.GetPageImage();
            dialogImage.color = Color.white;
        }
        else
        {
            dialogImage.sprite = null;
            dialogImage.color = Color.black;
        }
        dialogUI.LoadPage(currentPage.GetStartDialog(), bImpaired);
    }

    public void SwitchChoice(CallbackContext ctx)
    {
        if (dialogUI.GetOptionCount() == 0)
        {
            return;
        }
        int dir = (int)ctx.ReadValue<float>();
        int oldChoice = selectedIndex;
        selectedIndex = Mathf.Clamp(selectedIndex + dir, 0, dialogUI.GetOptionCount());
        dialogUI.UpdateChoice(oldChoice, selectedIndex);
    }

    public void NextDialog(CallbackContext ctx)
    {
        DialogObject dialogChoice = dialogUI.GetDialogChoice(selectedIndex);
        bImpaired = dialogChoice.DoesImpair();
        if (dialogChoice.DoesHeal() && bImpaired)
        {
            bImpaired = false;
        }
        Page nextPage = dialogChoice.GetNextPage();
        if (nextPage != null)
        {
            pageManager.NextPage(nextPage);
            LoadPage();
            return;
        }
        dialogUI.LoadDialog(dialogChoice.GetNextObjects(), bImpaired);
        selectedIndex = 0;
    }
}
