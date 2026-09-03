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

}