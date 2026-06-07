using UnityEngine;

public class ForceVectorVisualizer : MonoBehaviour
{
    [Header("Visualizer Settings")]
    public float forceScale = 500f;
    public Color forceColor = Color.red;

    private CelestialBody _body;
    private LineRenderer _line;

    private void Start()
    {
        _body = GetComponent<CelestialBody>();

        _line = GetComponent<LineRenderer>();
        if (_line == null)
            _line = gameObject.AddComponent<LineRenderer>();

        _line.positionCount = 2;
        _line.startWidth = 0.1f;
        _line.endWidth = 0.02f;
        _line.material = new Material(Shader.Find("Sprites/Default"));
        _line.startColor = forceColor;
        _line.endColor = new Color(forceColor.r, forceColor.g, forceColor.b, 0.3f);
        _line.useWorldSpace = true;
    }

    private void Update()
    {
        if (_body == null || _line == null) return;

        float forceMagnitude = _body.lastForce.magnitude;

        if (forceMagnitude < 1e-15f)
        {
            _line.enabled = false;
            return;
        }

        _line.enabled = true;

        Vector3 origin = transform.position;

        GameObject sun = GameObject.Find("Sun");
        Vector3 directionToSun = sun != null ?
            (sun.transform.position - origin).normalized :
            _body.lastForce.normalized;

        Vector3 forceEnd = origin + directionToSun * 3f;

        _line.SetPosition(0, origin);
        _line.SetPosition(1, forceEnd);
    }
}