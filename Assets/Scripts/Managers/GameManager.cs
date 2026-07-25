using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private CountdownController countdownController;
    [SerializeField] private EnemyGenerator enemyGenerator;

    // Singleton Instance
    public static GameManager instance { get; private set; }

    // Private Values
    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetPlayer() { return player; }

    public CountdownController GetCountdownController() { return countdownController; }

    public EnemyGenerator GetEnemyGenerator() { return enemyGenerator; }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void EndGame()
    {

    }
}
