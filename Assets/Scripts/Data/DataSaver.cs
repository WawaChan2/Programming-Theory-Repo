using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour {

  private MenuManager _menuManager;

  private void Start() {
    _menuManager = MenuManager.Instance;
  }

  public void Save() {
    Debug.Log("Saving...");

    SaveableObject[] saveableObjects = FindObjectsByType<SaveableObject>();
    List<TransformData> dataToBeSaved = new();

    foreach (var saveableObject in saveableObjects) {
      TransformData transformData = new() {
        Id = saveableObject.Id,
        Position = saveableObject.transform.position,
        Rotation = saveableObject.transform.rotation
      };

      dataToBeSaved.Add(transformData);
    }

    _menuManager.Save(dataToBeSaved);
  }
  
}