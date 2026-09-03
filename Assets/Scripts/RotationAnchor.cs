using UnityEngine;

public class RotationAnchor : MonoBehaviour {

  [SerializeField] private bool _isRotationXFixed;
  [SerializeField] private bool _isRotationYFixed;
  [SerializeField] private bool _isRotationZFixed;

  [SerializeField] private float _rotationXFixedValue;
  [SerializeField] private float _rotationYFixedValue;
  [SerializeField] private float _rotationZFixedValue;

  private void LateUpdate() {
    Vector3 rotation = transform.eulerAngles;

    if (_isRotationXFixed) rotation.x = _rotationXFixedValue;

    if (_isRotationYFixed) rotation.y = _rotationYFixedValue;

    if (_isRotationZFixed) rotation.z = _rotationZFixedValue;

    transform.rotation = Quaternion.Euler(rotation);
  }

}