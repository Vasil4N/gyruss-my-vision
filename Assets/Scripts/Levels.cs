using System.Collections;
using TMPro;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject EnemiesContainer;
    [SerializeField]
    private GameObject LevelTitle;
    [SerializeField]
    private GameObject ScoreText;
    [SerializeField]
    private Sprite[] enemySprites;

    private int spawnEnemiesCount = 0;
    private int currentEnemiesCount = 0;
    private int enemiesToSpawn = 0;
    private int enemySprite = 0;
    private int currentLevel = 0;
    private int score = 0;
    private float radiusVariationX = 1;
    private float radiusVariationY = 1;
    private float enemiesDirection = 1;

    private readonly int maxEnemiesToSpawn = 10;
    private readonly int enemiesIncrementByLevel = 2;

    private void Start()
    {
        startNextLevel();
    }

    public void enemyKilled(float enemySize)
    {
        currentEnemiesCount--;
        if (currentEnemiesCount <= 0) { startNextLevel(); }
        score += (int)(enemySize * 10);
        ScoreText.GetComponent<TMP_Text>().text = score.ToString("D6");
    }


    public void startNextLevel()
    {
        if (spawnEnemiesCount < maxEnemiesToSpawn)
        {
            spawnEnemiesCount += enemiesIncrementByLevel;
        }
        else
        {
            radiusVariationX = Random.Range(0.5f, 1f);
            radiusVariationY = Random.Range(0.5f, 1f);
        }
        enemiesDirection = Random.Range(0.0f, 2f);
        enemySprite = Random.Range(0, 5);
        enemiesToSpawn = 0;
        currentLevel++;
        LevelTitle.GetComponent<TMP_Text>().text = $"Stage: {currentLevel:D2}";
        StartCoroutine(SpawnEnemies());

    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesToSpawn < spawnEnemiesCount)
        {
            yield return new WaitForSeconds(0.3f);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(EnemyPrefab, Vector2.zero, Quaternion.identity, EnemiesContainer.transform);
        enemy.GetComponent<Enemy>().Initialize(radiusVariationX, radiusVariationY, enemiesDirection, enemySprites[enemySprite]);

        enemiesToSpawn++;
        currentEnemiesCount++;
    }
}
