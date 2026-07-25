using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    public Vector2 movementInput {  get; private set; } = Vector2.zero;

    // Private References
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
    }
}
