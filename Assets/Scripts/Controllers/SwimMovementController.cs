using UnityEngine;

public class SwimMovementController : MovementController, ISwimmable {

  protected override void Awake() {
    base.Awake();
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
    Swim();
  }

  public void Swim() {
    throw new System.NotImplementedException();
  }

}