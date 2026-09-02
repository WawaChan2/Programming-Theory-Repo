using UnityEngine;

public class WalkMovementController : MovementController, IWalkable {

  [SerializeField] private float _movementSpeed;

  private Rigidbody _rigidbody;

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

  protected override void Update() {
    base.Update();
  }

  private void FixedUpdate() {
    Walk();
  }

  private void OnTriggerEnter(Collider other) {
    if (other.TryGetComponent<Water>(out _)) enabled = false;
  }

  private void OnTriggerExit(Collider other) {
    if (other.TryGetComponent<Water>(out _)) enabled = true;
  }

  public void Walk() {
    Vector3 movement = new(_moveInput.x, 0, _moveInput.y);

    _rigidbody.MovePosition(_rigidbody.position + _movementSpeed * Time.fixedDeltaTime * movement);
  }

}