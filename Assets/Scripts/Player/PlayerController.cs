using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInput inputActions;

    private void Start()
    {
        DialogManager dialogManager = FindFirstObjectByType<DialogManager>();
        inputActions = new PlayerInput();
        inputActions.PageControls.Next.performed += dialogManager.NextDialog;
        inputActions.PageControls.Change.performed += dialogManager.SwitchChoice;
    }

    public void StartGame()
    {
        inputActions.PageControls.Enable();
    }
}
