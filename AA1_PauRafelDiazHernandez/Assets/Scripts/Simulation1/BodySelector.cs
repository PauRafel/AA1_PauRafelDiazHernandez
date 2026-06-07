using UnityEngine;
using UnityEngine.InputSystem;

public class BodySelector : MonoBehaviour
{
    [Header("References")]
    public NewtonianDataDisplay dataDisplay;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CelestialBody body = hit.collider.GetComponent<CelestialBody>();
            if (body != null)
                dataDisplay.SetSelectedBody(body);
        }
    }
}