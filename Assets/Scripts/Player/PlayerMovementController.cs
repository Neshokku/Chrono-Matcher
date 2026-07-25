using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerMovementController : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float topLimit = 4.5f;
    [SerializeField] private float bottomLimit = -4.5f;

    // Private Values


    // Private References
    private PlayerInputController playerInputController;

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
    }

    void Update()
    {
        float yInput = playerInputController.movementInput.y;

        float verticalMovement = (yInput != 0 ? Mathf.Sign(yInput) : 0.0f) * speed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y + verticalMovement, bottomLimit, topLimit));
    }
}
