using UnityEngine;

public class SwimMovementController : MovementController, ISwimmable {

  [SerializeField] private float _horizontalSpeed;
  [SerializeField] private float _verticalSpeed;

  [SerializeField] private float _rotationSmoothness;

  private Vector2 _moveInput;

  private float _ascendingInput;
  private float _descendingInput;

  private Rigidbody _rigidbody;

  private bool _isInWater;

  private float _lastAngleInDegree;

  protected override void Awake() {
    base.Awake();

    _rigidbody = GetComponentInChildren<Rigidbody>();

    _lastAngleInDegree = transform.rotation.y;
  }

  protected override void OnEnable() {
    base.OnEnable();
  }

  protected override void OnDisable() {
    base.OnDisable();
  }

  private void Update() {
    if (!_isInWater) return;

    _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

    _ascendingInput = _inputActions.Player.Jump.ReadValue<float>();
    _descendingInput = _inputActions.Player.Crouch.ReadValue<float>();

    UpdateFaceDirection();
  }

  private void FixedUpdate() {
    if (!_isInWater) return;

    Swim();
  }

  private void OnTriggerStay(Collider other) {
    if (other.TryGetComponent<Water>(out _)) {
      _rigidbody.useGravity = false;
      _isInWater = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.TryGetComponent<Water>(out _)) {
      _rigidbody.useGravity = true;
      _isInWater = false;
    }
  }

  public void Swim() {
    SwimHorizontally();
    SwimVertically();
  }

  private void SwimHorizontally() {
    Vector3 movement = new(_moveInput.x, 0, _moveInput.y);

    Vector3 horizontalVelocity = _horizontalSpeed * movement;

    _rigidbody.linearVelocity = new Vector3(horizontalVelocity.x, _rigidbody.linearVelocity.y, horizontalVelocity.z);
  }

  private void SwimVertically() {
    float verticalInput = _ascendingInput - _descendingInput;

    Vector3 verticalVelocity = _rigidbody.linearVelocity;
    verticalVelocity.y = verticalInput * _verticalSpeed;

    _rigidbody.linearVelocity = verticalVelocity;
  }

  private void UpdateFaceDirection() {
    if (_moveInput.x != 0 || _moveInput.y != 0) {
      float angleInRadian = -Mathf.Atan2(_moveInput.y, _moveInput.x) + Mathf.PI / 2;
      _lastAngleInDegree = angleInRadian * Mathf.Rad2Deg;
    }

    Quaternion targetRotation = Quaternion.Euler(0, _lastAngleInDegree, 0);

    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
  }

}