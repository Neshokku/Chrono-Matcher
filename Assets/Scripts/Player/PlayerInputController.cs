using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public Vector2 movementInput {  get; private set; } = Vector2.zero;

    // Private References
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GameManager.instance.GetPlayerInput();
    }

    void Update()
    {
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }
}
