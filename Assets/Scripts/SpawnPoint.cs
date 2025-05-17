using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private Transform _target;

    private void OnEnable()
    {
        _enemyPool.Getted += OnGetted;
        _enemyPool.Released += OnReleased;
    }

    private void OnDisable()
    {
        _enemyPool.Getted -= OnGetted;
        _enemyPool.Released -= OnReleased;
    }

    public void Spawn()
    {
        _enemyPool.GetEnemy(transform.position, Quaternion.identity);
    }

    private void OnGetted(Enemy enemy)
    {
        enemy.Initialize(_target);
    }

    private void OnReleased(Enemy enemy)
    {
        enemy.Reset();
    }
}