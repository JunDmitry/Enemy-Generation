using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MinInterval = 0.01f;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField, Min(MinInterval)] private float _spawnInterval = MinInterval;
    [SerializeField] private EnemyPool _enemyPool;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _enemyPool.Getted += OnGetted;
        _enemyPool.Released += OnReleased;
        _coroutine = StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        _enemyPool.Getted -= OnGetted;
        _enemyPool.Released -= OnReleased;
        StopCoroutine(_coroutine);
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitSeconds = new(_spawnInterval);

        while (enabled)
        {
            Vector3 spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            _enemyPool.GetEnemy(spawnPoint, Quaternion.identity);
            yield return waitSeconds;
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