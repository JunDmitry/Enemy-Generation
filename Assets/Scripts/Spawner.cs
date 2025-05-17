using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MinInterval = 0.01f;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField, Min(MinInterval)] private float _spawnInterval = MinInterval;
    [SerializeField] private EnemyPool _enemyPool;

    private WaitForSeconds _waitSeconds;

    private void Awake()
    {
        _waitSeconds = new(_spawnInterval);
    }

    private void OnEnable()
    {
        _enemyPool.Getted += OnGetted;
        _enemyPool.Released += OnReleased;
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        _enemyPool.Getted -= OnGetted;
        _enemyPool.Released -= OnReleased;
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            _enemyPool.GetEnemy(spawnPoint, Quaternion.identity);
            yield return _waitSeconds;
        }
    }

    private void OnGetted(Enemy enemy)
    {
        Vector2 direction = Random.insideUnitCircle.normalized;

        enemy.CollidedWithFence += OnCollidedWithFence;
        enemy.Initialize(new(direction.x, 0f, direction.y));
    }

    private void OnReleased(Enemy enemy)
    {
        enemy.CollidedWithFence -= OnCollidedWithFence;
        enemy.Reset();
    }

    private void OnCollidedWithFence(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }
}