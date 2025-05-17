using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MinInterval = 0.01f;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField, Min(MinInterval)] private float _spawnInterval = MinInterval;
    [SerializeField] private EnemyPool _enemyPool;

    private void OnEnable()
    {
        _enemyPool.Getting += OnGetting;
        _enemyPool.Releasing += OnReleasing;
        InvokeRepeating(nameof(Spawn), _spawnInterval, _spawnInterval);
    }

    private void OnDisable()
    {
        _enemyPool.Getting -= OnGetting;
        _enemyPool.Releasing -= OnReleasing;
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
        _enemyPool.GetEnemy(spawnPoint, Quaternion.identity);
    }

    private void OnGetting(Enemy enemy)
    {
        Vector2 direction = Random.insideUnitCircle.normalized;

        enemy.CollidedWithFence += OnCollidedWithFence;
        enemy.Initialize(new(direction.x, 0f, direction.y));
    }

    private void OnReleasing(Enemy enemy)
    {
        enemy.CollidedWithFence -= OnCollidedWithFence;
        enemy.Reset();
    }

    private void OnCollidedWithFence(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }
}