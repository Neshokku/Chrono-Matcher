using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemiesGeneratedAtATime = 2;
    [SerializeField] private float minTimeBetweenSpawns = 1.0f;
    [SerializeField] private float maxTimeBetweenSpawns = 2.0f;
    [SerializeField] private float timeForFirstSpawn = 1.0f;

    [SerializeField] private float verticalRange = 6.0f;
    [SerializeField] private float minHorizontalRange = 3.0f;
    [SerializeField] private float maxHorizontalRange = 7.0f;

    private int bonusPointValue = 1;
    private float bonusTime = 0.0f;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void Update()
    {
        if (bonusTime > 0.0f)
        {
            bonusTime -= Time.deltaTime;
        }
    }

    public void AddBonusTime(int newBonusPointValue, float addedBonusTime)
    {
        if (bonusTime <= 0.0f)
        {
            bonusTime = addedBonusTime;
            bonusPointValue = newBonusPointValue;
        } else
        {
            bonusTime += addedBonusTime;
            if (bonusPointValue < newBonusPointValue) bonusPointValue = newBonusPointValue;
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(timeForFirstSpawn);

        while (true)
        {
            int numberOfEnemiesGenerated = Random.Range(1, maxEnemiesGeneratedAtATime + 1);

            List<Vector2> generatePositions = new List<Vector2>();

            for (int i = 0; i < numberOfEnemiesGenerated; i++)
            {
                generatePositions.Add(
                    new Vector2(
                        Random.Range(minHorizontalRange, maxHorizontalRange) * Mathf.Sign(Random.Range(-1.0f, 1.0f)), 
                        verticalRange * Mathf.Sign(Random.Range(-1.0f, 1.0f))
                        )
                    );
            }

            foreach (Vector2 position in generatePositions)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();

                enemyController.Initialize(new Vector2(0.0f, -Mathf.Sign(position.y)));
                if (bonusTime > 0.0f)
                {
                    enemyController.SetPoints(bonusPointValue);
                    switch (bonusPointValue)
                    {
                        case 2:
                            enemyController.SetColor(Color.cyan);
                            break;
                        case 3:
                            enemyController.SetColor(Color.gold);
                            break;
                        default: break;
                    }
                }
            }

            float timeForNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

            yield return new WaitForSeconds(timeForNextSpawn);
        }
    }
}
