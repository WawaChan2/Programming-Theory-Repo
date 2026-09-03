using UnityEngine;

public class AnimalFollower : MonoBehaviour {

  [SerializeField] private MainManager _mainManager;

  [SerializeField] private Vector3 _offset;

  private void LateUpdate() {
    if (_mainManager.SelectedAnimal != null) transform.position = _mainManager.SelectedAnimal.transform.position + _offset;
  }

}