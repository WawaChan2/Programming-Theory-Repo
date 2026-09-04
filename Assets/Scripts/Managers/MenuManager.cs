using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MenuManager : MonoBehaviour {

  public static MenuManager Instance { get; private set; }

  public List<TransformData> LoadedDataStore { get; private set; }

  private string _savePath;

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    _savePath = Application.persistentDataPath + "/savefile.json";

    DontDestroyOnLoad(gameObject);
  }

  public void Save(params TransformData[] datas) {
    TransformDataRepository dataRepository = new() {
      transformDataStore = new()
    };

    foreach (var data in datas) dataRepository.transformDataStore.Add(data);

    string json = JsonUtility.ToJson(dataRepository);

    File.WriteAllText(_savePath, json);
  }

  public void Load() {
    if (!File.Exists(_savePath)) return;

    string json = File.ReadAllText(_savePath);
    TransformDataRepository dataRepository = JsonUtility.FromJson<TransformDataRepository>(json);

    LoadedDataStore = dataRepository.transformDataStore;
  }

}
