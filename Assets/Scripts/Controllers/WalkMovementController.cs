using UnityEngine;

public class WalkMovementController : MovementController, IWalkable {

  [SerializeField] private float _movementSpeed;

  [SerializeField] private float _rotationSmoothness;

  private Vector2 _moveInput;

  private Rigidbody _rigidbody;

  private bool _isInWater;

  private float _lastAngleInAngle;

  protected override void Awake() {
    base.Awake();

    _rigidbody = GetComponentInChildren<Rigidbody>();
  }

  protected override void OnEnable() {
    base.OnEnable();
  }

  protected override void OnDisable() {
    base.OnDisable();
  }

  private void Update() {
    if (_isInWater) return;

    _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

    UpdateFaceDirection();
  }

  private void FixedUpdate() {
    if (_isInWater) return;

    Walk();
  }

  private void OnTriggerStay(Collider other) {
    if (other.TryGetComponent<Water>(out _)) _isInWater = true;
  }

  private void OnTriggerExit(Collider other) {
    if (other.TryGetComponent<Water>(out _)) _isInWater = false;
  }

  public void Walk() {
    Vector3 movement = new(_moveInput.x, 0, _moveInput.y);

    Vector3 horizontalVelocity = _movementSpeed * movement;

    _rigidbody.linearVelocity = new Vector3(horizontalVelocity.x, _rigidbody.linearVelocity.y, horizontalVelocity.z);
  }

  private void UpdateFaceDirection() {
    if (_moveInput.x != 0 || _moveInput.y != 0) {
      float angleInRadian = -Mathf.Atan2(_moveInput.y, _moveInput.x) + Mathf.PI / 2;
      _lastAngleInAngle = angleInRadian * Mathf.Rad2Deg;
    }

    Quaternion targetRotation = Quaternion.Euler(0, _lastAngleInAngle, 0);

    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSmoothness * Time.deltaTime);
  }

}