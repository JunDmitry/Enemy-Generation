using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _target;

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.fixedDeltaTime);
    }

    public void Reset()
    {
        _target = null;
    }

    public void Initialize(Transform target)
    {
        _target = target;
    }
}