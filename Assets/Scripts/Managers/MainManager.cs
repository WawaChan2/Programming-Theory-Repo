using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

  public static MainManager Instance { get; private set; }

  public Animal SelectedAnimal { get; private set; }

  public event Action OnAnimalSelected;

  private InputSystem_Actions _inputActions;

  private void Awake() {
    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    _inputActions = new InputSystem_Actions();
  }

  private void OnEnable() {
    _inputActions.Enable();

    _inputActions.UI.Click.performed += HandleClickInput;
  }

  private void OnDisable() {
    _inputActions.Disable();

    _inputActions.UI.Click.performed -= HandleClickInput;
  }

  public void ReturnToMenu() {
    SceneManager.LoadScene(0);
  }

  private void HandleClickInput(InputAction.CallbackContext context) {
    Vector2 mousePosition = _inputActions.Player.Point.ReadValue<Vector2>();

    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
    int layerMask = ~(1 << LayerMask.NameToLayer("Pool"));

    if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask) && hitInfo.transform.TryGetComponent<Animal>(out var hitAnimal)) {
      if (SelectedAnimal != null) DeselectCurrentAnimal();

      SelectNewAnimal(hitAnimal);
    }
  }

  private void SelectNewAnimal(Animal animal) {
    MovementController[] movementControllers = animal.GetComponentsInChildren<MovementController>();
    CinemachineCamera cinemachineCamera = animal.GetComponentInChildren<CinemachineCamera>();

    foreach (var movementController in movementControllers) movementController.enabled = true;
    cinemachineCamera.Priority = 10;

    SelectedAnimal = animal;

    OnAnimalSelected?.Invoke();
  }

  private void DeselectCurrentAnimal() {
    MovementController[] movementControllers = SelectedAnimal.GetComponentsInChildren<MovementController>();
    CinemachineCamera cinemachineCamera = SelectedAnimal.GetComponentInChildren<CinemachineCamera>();

    foreach (var movementController in movementControllers) movementController.enabled = false;
    cinemachineCamera.Priority = 0;

    SelectedAnimal = null;
  }
  
}