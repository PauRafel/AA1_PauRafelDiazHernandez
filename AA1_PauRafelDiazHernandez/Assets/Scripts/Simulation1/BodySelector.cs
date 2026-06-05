using UnityEngine;

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
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CelestialBody body = hit.collider.GetComponent<CelestialBody>();
            if (body != null)
                dataDisplay.SetSelectedBody(body);
        }
    }
}