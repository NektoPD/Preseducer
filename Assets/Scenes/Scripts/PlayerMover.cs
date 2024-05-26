using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _lookSpeed;
    [SerializeField] private float _verticalMinAngle = -89f;
    [SerializeField] private float _verticalMaxAngle = 90f;

    private Transform _transform;
    private Transform _cameraTransform;
    private PlayerInputLayout _inputLayout;
    private Vector2 _moveDirection;
    private Vector2 _lookDirection;
    private CharacterController _characterController;
    private float _cameraAngle = 0;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _cameraTransform = Camera.main.transform;
        _inputLayout = new PlayerInputLayout();
        _characterController = GetComponent<CharacterController>();
        _cameraAngle = _cameraTransform.localEulerAngles.x;
    }

    private void Update()
    {
        _moveDirection = _inputLayout.PlayerInput.Move.ReadValue<Vector2>();
        _lookDirection = _inputLayout.PlayerInput.Look.ReadValue<Vector2>();

        Move();
        Look();
    }

    private void OnEnable()
    {
        _inputLayout.Enable();
    }

    private void OnDisable()
    {
        _inputLayout.Disable();
    }

    private void Move()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 cameraForward = _cameraTransform.forward.normalized;
        Vector3 cameraRight = _cameraTransform.right;
        Vector3 moveDirection = (cameraForward * _moveDirection.y + cameraRight * _moveDirection.x).normalized;
        Vector3 offset = moveDirection * scaledMoveSpeed;

        if(_characterController.isGrounded)
        {
            _characterController.Move(offset + Vector3.down);
        }
        else
        {
            _characterController.Move(_characterController.velocity + Physics.gravity * Time.deltaTime);
        }
    }

    private void Look()
    {
        _cameraAngle -= _lookDirection.y * _lookSpeed;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);

        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;
        _transform.Rotate(Vector3.up * _lookSpeed * _lookDirection.x);
    }
}

