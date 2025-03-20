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

    private int _spawnCount = 0; // Đếm số lượng enemy đã spawn
    private int _maxSpawnCount = 10; // Số lượng enemy tối đa trước khi hủy spawner

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
        _spawnCount++; // Tăng bộ đếm số enemy đã spawn

        if (_spawnCount >= _maxSpawnCount)
        {
            Destroy(gameObject); // Xóa Spawner sau khi spawn đủ 10 enemy
        }
        else
        {
            SetTimeUntilSpawn(); // Reset bộ đếm spawn nếu chưa đủ số lượng
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);
    }
}
