using UnityEngine;

public class SaveableObject : MonoBehaviour {

  public string Id => _id;

  [SerializeField] private string _id;
  
}