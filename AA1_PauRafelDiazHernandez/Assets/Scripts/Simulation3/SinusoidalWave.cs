using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SinusoidalWave : MonoBehaviour, IResettable
{
    [Header("Wave Settings")]
    public WaveParameters[] waves;

    private Mesh _mesh;
    private Vector3[] _baseVertices;
    private Vector3[] _currentVertices;

    private void Awake()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        _baseVertices = _mesh.vertices;
        _currentVertices = new Vector3[_baseVertices.Length];
    }

    private void Update()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        UpdateVertices();
    }

    private void UpdateVertices()
    {
        float time = Time.time;

        for (int i = 0; i < _baseVertices.Length; i++)
        {
            Vector3 baseVertex = _baseVertices[i];
            float y = 0f;

            foreach (var wave in waves)
            {
                Vector2 dir = wave.NormalizedDirection;
                float x = baseVertex.x * dir.x + baseVertex.z * dir.y;

                y += wave.amplitude * Mathf.Sin(
                    wave.WaveNumber * x - wave.AngularFrequency * time + wave.phase
                );
            }

            _currentVertices[i] = new Vector3(baseVertex.x, y, baseVertex.z);
        }

        _mesh.vertices = _currentVertices;
        _mesh.RecalculateNormals();
    }

    public float GetHeightAt(float x, float z)
    {
        float time = Time.time;
        float y = 0f;

        foreach (var wave in waves)
        {
            Vector2 dir = wave.NormalizedDirection;
            float pos = x * dir.x + z * dir.y;

            y += wave.amplitude * Mathf.Sin(
                wave.WaveNumber * pos - wave.AngularFrequency * time + wave.phase
            );
        }

        return y;
    }

    public void ResetSimulation()
    {
        _mesh.vertices = _baseVertices;
        _mesh.RecalculateNormals();
    }
}