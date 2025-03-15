using UnityEngine;

public class EnemyWaveManager : Singleton<EnemyWaveManager>
{
    private float nextEnemySpawnTimer = 1.2f;
    private Vector3 spawnPosition;

    private Transform enemyObject;
    [SerializeField] private EnemiesSO[] enemies;
    [HideInInspector] public float elapsedTime { get; private set; }
    float difficultyMultiplier;

    private void Update()
    {
        elapsedTime = Time.time;

        nextEnemySpawnTimer -= Time.deltaTime;
        if (nextEnemySpawnTimer < 0)
            SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        do
        {
           spawnPosition = new Vector3(Random.Range(-80, 80), Random.Range(-80, 80));
        } while (spawnPosition.x > -20f && spawnPosition.x < 20f && spawnPosition.y > -20f && spawnPosition.y < 20f);

        CreateEnemy(spawnPosition);

        difficultyMultiplier = Mathf.Clamp(1 / (1 + elapsedTime * 0.05f), 0.2f, 1f);
        nextEnemySpawnTimer = 1.1f * Random.Range(1, 12) * difficultyMultiplier;
    }
    private void CreateEnemy(Vector3 position)
    {
        Transform _enemy = Resources.Load<Transform>(GetRandomEnemyName());

        if (_enemy.gameObject.GetComponent<Enemy>().CanSpawn(position))
             enemyObject = Instantiate(_enemy, position, Quaternion.identity);

    }
    public float GetElapsedTime()
    {
        return Mathf.Floor((elapsedTime / 60));
    }

    private string GetRandomEnemyName()
    {
        int elapsedMinutes = (int)Mathf.Floor(elapsedTime / 60);
        int weightedIndex = Mathf.Clamp((int)elapsedMinutes, 0, enemies.Length - 1);
        int randomIndex = Random.Range(0, weightedIndex + 1);
        return enemies[randomIndex].name;
    }

}
