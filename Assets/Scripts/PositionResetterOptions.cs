using UnityEngine;

public class PositionResetterOptions : MonoBehaviour {

  public Vector3 TargetPosition => _targetPosition;

  [SerializeField] private Vector3 _targetPosition;
  
}