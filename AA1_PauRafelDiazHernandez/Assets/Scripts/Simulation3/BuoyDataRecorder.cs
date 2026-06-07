using UnityEngine;

public class BuoyDataRecorder : MonoBehaviour, IResettable
{
    [Header("References")]
    public BuoyancyObject buoy;
    public PhysicsDataRecorder recorder;

    [Header("Settings")]
    public float sampleInterval = 0.1f;

    private float _timer = 0f;
    private Vector3 _previousPosition;

    private void Start()
    {
        _previousPosition = buoy.transform.position;
    }

    private void FixedUpdate()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        if (buoy == null || recorder == null) return;

        _timer += Time.fixedDeltaTime;
        if (_timer < sampleInterval) return;
        _timer = 0f;

        float velocity = Vector3.Distance(buoy.transform.position, _previousPosition)
                         / sampleInterval;

        float waterDensity = buoy.physicsConfig != null ?
            buoy.physicsConfig.waterDensity : 1000f;
        float gravity = buoy.physicsConfig != null ?
            buoy.physicsConfig.earthGravity : 9.81f;

        float force = waterDensity * gravity * buoy.objectVolume;

        recorder.RecordSample(velocity, force);

        _previousPosition = buoy.transform.position;
    }

    public void ResetSimulation()
    {
        _timer = 0f;
        recorder?.Reset();
    }
}