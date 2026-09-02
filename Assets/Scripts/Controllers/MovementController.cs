using UnityEngine;

public abstract class MovementController : MonoBehaviour {

  protected InputSystem_Actions _inputActions;

  protected Vector2 _moveInput;

  protected virtual void Awake() {
    _inputActions = new InputSystem_Actions();
  }

  protected virtual void OnEnable() {
    _inputActions.Enable();
  }

  protected virtual void OnDisable() {
    _inputActions.Disable();
  }

  protected virtual void Update() {
    _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
  }

}