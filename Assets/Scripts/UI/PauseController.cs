using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool paused { get; private set; } = false;

    private void Start()
    {
        pauseMenu.SetActive(paused);
    }

    private void TogglePause()
    {
        paused = !paused;
        pauseMenu.SetActive(paused);
        Time.timeScale = paused ? 0.0f : 1.0f;
    }

    public void TogglePauseTrigger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TogglePause();
        }
    }

}
