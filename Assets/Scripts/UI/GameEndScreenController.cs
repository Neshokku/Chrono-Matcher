using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameEndScreenController : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI textMeshProUGUIHighScore;


    [Header("Sounds")]
    [SerializeField] private AudioClip endSound;
    [SerializeField] private AudioClip retrySound;

    public void ShowScreen(int score)
    {
        int highScore = 0;

        if (PlayerPrefs.HasKey("HighScore")) 
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }

        bool newHighScore = score > highScore;

        SoundManager.instance.PlaySFX(endSound);
        menuScreen.SetActive(true);
        animator.SetTrigger("Close");
        textMeshProUGUI.text = "Score: " + score.ToString();
        textMeshProUGUIHighScore.text = (newHighScore ? " (New High Score!)" : " (High Score: " + highScore + ")");

        if (newHighScore) {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void RestartGame(InputAction.CallbackContext context)
    {
        if (context.performed && !GameManager.instance.gameRunning)
        {
            SoundManager.instance.PlaySFX(retrySound);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
