using UnityEngine;

public class WalkMovementController : MovementController, IWalkable {

  [SerializeField] private float _movementSpeed;

  private Vector2 _moveInput;

  private Rigidbody _rigidbody;

  private bool _isInWater;

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
    float angleInRadian;

    if (_moveInput.y == 0 && _moveInput.x == 0) angleInRadian = 0;
    else angleInRadian = -Mathf.Atan2(_moveInput.y, _moveInput.x) + Mathf.PI / 2;

    float angleInDegree = angleInRadian * Mathf.Rad2Deg;

    transform.rotation = Quaternion.Euler(0, angleInDegree, 0);
  }

}