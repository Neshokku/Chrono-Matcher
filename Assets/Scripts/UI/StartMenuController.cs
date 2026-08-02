using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [Header("Sounds")]
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioClip clickSound;

    public void StartGame()
    {
        SoundManager.instance.PlaySFX(clickSound);
        SoundManager.instance.PlaySFX(startSound);
        animator.SetTrigger("TriggerGameStart");
    }

    public void LoadGameScene()
    {
        Debug.Log("ChangingScene");
        SceneManager.LoadScene("MainScene");
    }
}
