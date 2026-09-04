using UnityEngine;

public class MenuManager : MonoBehaviour {

  public static MenuManager Instance { get; private set; }

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    DontDestroyOnLoad(gameObject);
  }

}
