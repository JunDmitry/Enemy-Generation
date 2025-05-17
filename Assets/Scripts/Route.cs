using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private Transform[] _points;

    private int _pointIndex = 0;

    public Vector3 Target => _points[_pointIndex].position;

    public void NextTarget()
    {
        _pointIndex = (_pointIndex + 1) % _points.Length;
    }

    [ContextMenu(nameof(Shuffle))]
    private void Shuffle()
    {
        int randomIndex;
        Transform current;

        for (int i = 0; i < _points.Length; i++)
        {
            randomIndex = Random.Range(0, _points.Length);

            current = _points[i];
            _points[i] = _points[randomIndex];
            _points[randomIndex] = current;
        }
    }
}