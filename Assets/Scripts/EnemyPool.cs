using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private int _initialCount;

    private Stack<Enemy> _pool;

    public event Action<Enemy> Getting;

    public event Action<Enemy> Releasing;

    private void Awake()
    {
        _pool = new(_initialCount);
    }

    private void Start()
    {
        while (_pool.Count < _initialCount)
            _pool.Push(_enemyFactory.Create());
    }

    public Enemy GetEnemy(Vector3 position, Quaternion rotation)
    {
        Enemy enemy = _pool.Count == 0 ? _enemyFactory.Create() : _pool.Pop();
        enemy.transform.SetPositionAndRotation(position, rotation);
        enemy.gameObject.SetActive(true);
        Getting?.Invoke(enemy);

        return enemy;
    }

    public void Release(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        _pool.Push(enemy);
        Releasing?.Invoke(enemy);
    }
}