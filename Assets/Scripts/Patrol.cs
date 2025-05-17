using UnityEngine;

public class Patrol : MonoBehaviour
{
    private const float Epsilon = 0.001f;

    [SerializeField] private Route _route;
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Vector3 target = _route.Target;
        float distance = _speed * Time.fixedDeltaTime;

        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, distance);

        if (IsArriveToTarget(target))
            _route.NextTarget();
    }

    private bool IsArriveToTarget(Vector3 target)
    {
        return (transform.position - target).sqrMagnitude < Epsilon * Epsilon;
    }
}