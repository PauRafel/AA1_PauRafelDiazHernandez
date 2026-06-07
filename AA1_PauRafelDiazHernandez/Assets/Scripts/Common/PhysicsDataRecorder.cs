using UnityEngine;
using System.Collections.Generic;

public class PhysicsDataRecorder : MonoBehaviour
{
    public int maxSamples = 200;

    private List<float> _velocitySamples = new List<float>();
    private List<float> _forceSamples = new List<float>();
    private List<float> _displacementSamples = new List<float>();

    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    public void RecordSample(float velocity, float force)
    {
        float displacement = Vector3.Distance(transform.position, _initialPosition);

        AddSample(_velocitySamples, velocity);
        AddSample(_forceSamples, force);
        AddSample(_displacementSamples, displacement);
    }

    private void AddSample(List<float> list, float value)
    {
        list.Add(value);
        if (list.Count > maxSamples)
            list.RemoveAt(0);
    }

    public List<float> GetVelocitySamples() => _velocitySamples;
    public List<float> GetForceSamples() => _forceSamples;
    public List<float> GetDisplacementSamples() => _displacementSamples;

    public void Reset()
    {
        _velocitySamples.Clear();
        _forceSamples.Clear();
        _displacementSamples.Clear();
        _initialPosition = transform.position;
    }
}