using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

  private MenuManager _menuManager;

  private void Start() {
    _menuManager = MenuManager.Instance;

    Load();
  }

  private void Load() {
    Debug.Log("Loading...");

    _menuManager.Load();

    List<TransformData> loadedData = _menuManager.LoadedDataStore;
    if (loadedData == null) return;

    SaveableObject[] saveableObjects = FindObjectsByType<SaveableObject>();

    foreach (var saveableObject in saveableObjects) {
      foreach (var data in loadedData) {
        if (saveableObject.Id == data.Id) saveableObject.transform.SetPositionAndRotation(data.Position, data.Rotation);
      }
    }
  }
  
}