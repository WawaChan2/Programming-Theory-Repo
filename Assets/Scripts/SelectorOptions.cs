using UnityEngine;

public class SelectorOptions : MonoBehaviour {

  public Vector3 SelectorOffset => _selectorOffset;
  public float SelectorScale => _selectorScale;

  [SerializeField] private Vector3 _selectorOffset;
  [SerializeField] private float _selectorScale;

}