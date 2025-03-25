using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private float _minimumSpawnTime;

    [SerializeField]
    private float _maximumSpawnTime;

    [SerializeField]
    private float _timeUntilSpawn;

    private int _spawnCount = 0; // Spawn enemies count
    private int _maxSpawnCount = 5; // Max enemies to spawn

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        _spawnCount++;

        if (_spawnCount >= _maxSpawnCount)
        {
            Destroy(gameObject);
        }
        else
        {
            SetTimeUntilSpawn(); // Reset timer
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
