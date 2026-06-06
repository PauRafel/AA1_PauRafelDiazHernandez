using UnityEngine;

public class BuoyancyObject : MonoBehaviour, IResettable
{
    [Header("Buoyancy Properties")]
    public float waterDensity = 1000f;
    public float objectVolume = 0.1f;
    public float objectMass = 5f;
    public float damping = 0.8f;

    [Header("Wave Mode")]
    public bool useGerstner = true;

    [Header("References")]
    public GerstnerWave gerstnerWave;
    public SinusoidalWave sinusoidalWave;

    private Vector3 _initialPosition;
    private Vector3 _velocity = Vector3.zero;
    private const float Gravity = 9.81f;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        float waterHeight = GetWaterHeight();
        float buoyancyForce = CalculateBuoyancy(waterHeight);

        ApplyPhysics(buoyancyForce);
    }

    private float GetWaterHeight()
    {
        float x = transform.position.x;
        float z = transform.position.z;

        if (useGerstner && gerstnerWave != null)
            return gerstnerWave.GetHeightAt(x, z);

        if (!useGerstner && sinusoidalWave != null)
            return sinusoidalWave.GetHeightAt(x, z);

        return 0f;
    }

    private float CalculateBuoyancy(float waterHeight)
    {
        float submergedDepth = Mathf.Max(0f, waterHeight - transform.position.y);
        float submergedVolume = Mathf.Min(submergedDepth * objectVolume, objectVolume);

        return waterDensity * Gravity * submergedVolume;
    }

    private void ApplyPhysics(float buoyancyForce)
    {
        float gravityForce = objectMass * Gravity;
        float netForce = buoyancyForce - gravityForce;
        float acceleration = netForce / objectMass;

        _velocity.y += acceleration * Time.fixedDeltaTime;
        _velocity.y *= damping;

        transform.position += _velocity * Time.fixedDeltaTime;
    }

    public void ResetSimulation()
    {
        transform.position = _initialPosition;
        _velocity = Vector3.zero;
    }
}