using UnityEngine;
using UnityEngine.InputSystem;

public class FlyMovementController : MovementController, IFlyable {

  [SerializeField] private float _airLaunchSpeed;

  private bool _isAirLaunchRequested;

  private Rigidbody _rigidbody;

  private bool _isInWater;

  protected override void Awake() {
    base.Awake();

    _rigidbody = GetComponentInChildren<Rigidbody>();
  }

  protected override void OnEnable() {
    base.OnEnable();

    _inputActions.Player.Jump.performed += HandleJumpInput;
  }

  protected override void OnDisable() {
    base.OnDisable();

    _inputActions.Player.Jump.performed -= HandleJumpInput;
  }

  private void FixedUpdate() {
    if (_isInWater) return;

    Fly();
  }

  private void OnTriggerStay(Collider other) {
    if (other.TryGetComponent<Water>(out _)) _isInWater = true;
  }

  private void OnTriggerExit(Collider other) {
    if (other.TryGetComponent<Water>(out _)) _isInWater = false;
  }

  public void Fly() {
    if (_isAirLaunchRequested) {
      ZeroOutYComponentVelocity();

      _rigidbody.AddForce(_airLaunchSpeed * Vector3.up, ForceMode.VelocityChange);

      _isAirLaunchRequested = false;
    }
  }

  private void HandleJumpInput(InputAction.CallbackContext _) => _isAirLaunchRequested = true;

  private void ZeroOutYComponentVelocity() {
    Vector3 newVelocity = _rigidbody.linearVelocity;
    newVelocity.y = 0;

    _rigidbody.linearVelocity = newVelocity;
  }

}