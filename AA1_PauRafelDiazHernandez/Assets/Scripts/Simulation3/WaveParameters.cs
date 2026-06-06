using UnityEngine;

[System.Serializable]
public class WaveParameters
{
    [Header("Wave Properties")]
    public float amplitude = 0.5f;
    public float wavelength = 2f;
    public float frequency = 1f;
    public float phase = 0f;
    public Vector2 direction = Vector2.right;

    public float WaveNumber => (2f * Mathf.PI) / wavelength;
    public float AngularFrequency => 2f * Mathf.PI * frequency;

    public Vector2 NormalizedDirection => direction.normalized;
}