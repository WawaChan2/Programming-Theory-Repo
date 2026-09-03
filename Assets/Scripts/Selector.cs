using UnityEngine;

public class Selector : MonoBehaviour {

  [SerializeField] private Vector3 _defaultOffset;
  [SerializeField] private float _defaultScale;

  private MainManager _mainManager;

  private Vector3 _offset;

  private void Awake() {
    _mainManager = MainManager.Instance;
  }

  private void OnEnable() {
    _mainManager.OnAnimalSelected += UpdateSelector;
  }

  private void OnDisable() {
    _mainManager.OnAnimalSelected -= UpdateSelector;
  }

  private void LateUpdate() {
    if (_mainManager.SelectedAnimal != null) transform.position = _mainManager.SelectedAnimal.transform.position + _offset;
  }

  private void UpdateSelector() {
    if (_mainManager.SelectedAnimal.TryGetComponent<SelectorOptions>(out var selectorOptions)) {
      _offset = selectorOptions.SelectorOffset;
      transform.localScale = selectorOptions.SelectorScale * Vector3.one;

      return;
    }

    _offset = _defaultOffset;
    transform.localScale = _defaultScale * Vector3.one;
  }

}