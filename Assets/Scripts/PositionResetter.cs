using UnityEngine;

public class PositionResetter : MonoBehaviour {

  [SerializeField] private Vector3 _defaultTargetPosition;

  private MainManager _mainManager;

  private void Start() {
    _mainManager = MainManager.Instance;
  }

  public void TeleportToPosition() {
    Animal selectedAnimal = _mainManager.SelectedAnimal;
    if (selectedAnimal == null) return;

    if (selectedAnimal.TryGetComponent<PositionResetterOptions>(out var positionResetterOptions)) {
      selectedAnimal.transform.position = positionResetterOptions.TargetPosition;

      return;
    }

    selectedAnimal.transform.position = _defaultTargetPosition;
  }
  
}