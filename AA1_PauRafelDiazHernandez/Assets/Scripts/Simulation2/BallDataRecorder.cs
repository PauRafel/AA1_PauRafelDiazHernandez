using UnityEngine;

public class BallDataRecorder : MonoBehaviour, IResettable
{
    [Header("References")]
    public BallPhysics ball;
    public PhysicsDataRecorder recorder;

    [Header("Settings")]
    public float sampleInterval = 0.1f;

    private float _timer = 0f;
    private float _lastForce = 0f;

    private void FixedUpdate()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        if (ball == null || recorder == null) return;

        _timer += Time.fixedDeltaTime;
        if (_timer < sampleInterval) return;
        _timer = 0f;

        float velocity = ball.velocity.magnitude;
        float force = ball.mass * (velocity - _lastForce) / sampleInterval;
        _lastForce = velocity;

        recorder.RecordSample(velocity, Mathf.Abs(force));
    }

    public void ResetSimulation()
    {
        _timer = 0f;
        _lastForce = 0f;
        recorder.Reset();
    }
}