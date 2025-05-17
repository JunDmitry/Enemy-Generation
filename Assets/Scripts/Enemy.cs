using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _direction = Vector3.zero;

    public event Action<Enemy> CollidedWithFence;

    private void FixedUpdate()
    {
        Vector3 distance = _speed * Time.fixedDeltaTime * _direction;

        transform.LookAt(transform.position + distance);
        transform.Translate(distance, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Fence _))
            CollidedWithFence?.Invoke(this);
    }

    public void Reset()
    {
        _direction = Vector3.zero;
    }

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }
}