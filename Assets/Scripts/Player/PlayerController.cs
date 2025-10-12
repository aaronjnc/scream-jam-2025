using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInput inputActions;

    private void Awake()
    {
        inputActions = new PlayerInput();
    }
}
