using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GerstnerWave : MonoBehaviour, IResettable
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
            float x = baseVertex.x;
            float z = baseVertex.z;
            float y = 0f;
            float offsetX = 0f;
            float offsetZ = 0f;

            foreach (var wave in waves)
            {
                Vector2 dir = wave.NormalizedDirection;
                float k = wave.WaveNumber;
                float w = wave.AngularFrequency;
                float A = wave.amplitude;

                float dot = dir.x * baseVertex.x + dir.y * baseVertex.z;
                float phase = k * dot - w * time + wave.phase;

                y += A * Mathf.Sin(phase);
                offsetX += (A / k) * dir.x * Mathf.Cos(phase);
                offsetZ += (A / k) * dir.y * Mathf.Cos(phase);
            }

            _currentVertices[i] = new Vector3(x + offsetX, y, z + offsetZ);
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
            float dot = dir.x * x + dir.y * z;
            float phase = wave.WaveNumber * dot - wave.AngularFrequency * time + wave.phase;
            y += wave.amplitude * Mathf.Sin(phase);
        }

        return y;
    }

    public void ResetSimulation()
    {
        _mesh.vertices = _baseVertices;
        _mesh.RecalculateNormals();
    }
}