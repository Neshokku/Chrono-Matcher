using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private CountdownController countdownController;
    [SerializeField] private EnemyGenerator enemyGenerator;
    [SerializeField] private PauseController pauseController;
    [SerializeField] private GameEndScreenController gameEndScreenController;
    [SerializeField] private PlayerInput playerInput;

    // Singleton Instance
    public static GameManager instance { get; private set; }

    // Private Values
    public int score { get; private set; } = 0;
    public bool gameRunning { get; private set; } = true; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public bool IsPaused() { return pauseController.paused; }

    public GameObject GetPlayer() { return player; }

    public CountdownController GetCountdownController() { return countdownController; }

    public EnemyGenerator GetEnemyGenerator() { return enemyGenerator; }

    public PauseController GetPauseController() { return pauseController; }

    public PlayerInput GetPlayerInput() { return playerInput; }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void EndGame()
    {
        gameRunning = false;
        gameEndScreenController.ShowScreen(score);
        countdownController.enabled = false;
        enemyGenerator.enabled = false;
        player.SetActive(false);
    }
}
