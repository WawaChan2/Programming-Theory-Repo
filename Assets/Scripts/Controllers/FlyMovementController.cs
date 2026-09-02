using UnityEngine;
using UnityEngine.InputSystem;

public class FlyMovementController : MovementController, IFlyable {

  [SerializeField] private float _airLaunchSpeed;

  private Rigidbody _rigidbody;

  private bool _isAirLaunchRequested;

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

  protected override void Update() {
    base.Update();
  }

  private void FixedUpdate() {
    Fly();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent<Water>(out _)) enabled = false;
  }

  private void OnTriggerExit(Collider other) {
    if (other.TryGetComponent<Water>(out _)) enabled = true;
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