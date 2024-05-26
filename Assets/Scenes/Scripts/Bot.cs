using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bot : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _playerPosition;

    private float _speed = 3;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 targetPosition = _playerPosition.position + _offset;
        Vector3 direction = (targetPosition - transform.position).normalized;

        Vector3 horizontalVelocity = new Vector3(direction.x, 0, direction.z) * _speed;

        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 newVelocity = new Vector3(horizontalVelocity.x, currentVelocity.y, horizontalVelocity.z);

        _rigidbody.velocity = newVelocity;
    }
}
