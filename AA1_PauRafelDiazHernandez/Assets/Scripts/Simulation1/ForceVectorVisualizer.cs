using UnityEngine;

public class ForceVectorVisualizer : MonoBehaviour
{
    [Header("Visualizer Settings")]
    public float forceScale = 0.01f;
    public Color forceColor = Color.red;

    private CelestialBody _body;

    private void Awake()
    {
        _body = GetComponent<CelestialBody>();
    }

    private void Update()
    {
        if (_body == null) return;

        Vector3 origin = transform.position;
        Vector3 forceEnd = origin + _body.currentForce * forceScale;

        Debug.DrawLine(origin, forceEnd, forceColor);
    }
}