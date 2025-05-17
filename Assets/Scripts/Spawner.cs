using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MinInterval = 0.01f;

    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField, Min(MinInterval)] private float _spawnInterval = MinInterval;

    private WaitForSeconds _waitSeconds;

    private void Awake()
    {
        _waitSeconds = new(_spawnInterval);
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            _spawnPoints[Random.Range(0, _spawnPoints.Length)].Spawn();
            yield return _waitSeconds;
        }
    }
}