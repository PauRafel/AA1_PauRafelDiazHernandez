using UnityEngine;

public class NewtonianDataRecorder : MonoBehaviour, IResettable
{
    [Header("References")]
    public CelestialBody trackedBody;
    public PhysicsDataRecorder recorder;

    [Header("Settings")]
    public float sampleInterval = 0.1f;

    private float _timer = 0f;

    private void FixedUpdate()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        if (trackedBody == null || recorder == null) return;

        _timer += Time.fixedDeltaTime;
        if (_timer < sampleInterval) return;
        _timer = 0f;

        float velocity = trackedBody.velocity.magnitude;
        float force = trackedBody.lastForce.magnitude;

        recorder.RecordSample(velocity, force);
    }

    public void ResetSimulation()
    {
        _timer = 0f;
        recorder?.Reset();
    }
}