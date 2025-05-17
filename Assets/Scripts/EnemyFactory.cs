using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _container;

    public Enemy Create()
    {
        Enemy enemy = Instantiate(_prefab, _container);
        enemy.gameObject.SetActive(false);

        return enemy;
    }
}