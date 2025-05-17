using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private const float Epsilon = 0.001f;

    [SerializeField] private float _speed;

    private Vector3 _direction = Vector3.zero;
    private Coroutine _coroutine;

    public event Action<Enemy> CollidedWithFence;

    private void FixedUpdate()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        Vector3 target = transform.position + _speed * Time.fixedDeltaTime * _direction;
        transform.LookAt(target);

        _coroutine = StartCoroutine(MoveTo(target));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Fence _))
            CollidedWithFence?.Invoke(this);
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void Reset()
    {
        _direction = Vector3.zero;
        _coroutine = null;
    }

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    private IEnumerator MoveTo(Vector3 target)
    {
        while ((target - transform.position).sqrMagnitude >= Epsilon * Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }
    }
}