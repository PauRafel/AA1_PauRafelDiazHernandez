using UnityEngine;

public class WaveStabilityChecker : MonoBehaviour
{
    [Header("References")]
    public GerstnerWave gerstnerWave;
    public SinusoidalWave sinusoidalWave;

    [Header("Stability Limits")]
    public float maxAmplitude = 5f;
    public float minWavelength = 0.5f;
    public float maxFrequency = 10f;
    public float checkInterval = 1f;

    private float _timer = 0f;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < checkInterval) return;
        _timer = 0f;

        CheckWaves(gerstnerWave?.waves, "Gerstner");
        CheckWaves(sinusoidalWave?.waves, "Sinusoidal");
    }

    private void CheckWaves(WaveParameters[] waves, string modelName)
    {
        if (waves == null) return;

        foreach (var wave in waves)
        {
            if (wave.amplitude > maxAmplitude)
                Debug.LogWarning($"[{modelName}] Amplitude {wave.amplitude:F2} " +
                                 $"exceeds max {maxAmplitude}");

            if (wave.wavelength < minWavelength)
                Debug.LogWarning($"[{modelName}] Wavelength {wave.wavelength:F2} " +
                                 $"below min {minWavelength}");

            if (wave.frequency > maxFrequency)
                Debug.LogWarning($"[{modelName}] Frequency {wave.frequency:F2} Hz " +
                                 $"exceeds max {maxFrequency}");

            if (wave.amplitude > wave.wavelength / (2f * Mathf.PI))
                Debug.LogWarning($"[{modelName}] Wave steepness too high! " +
                                 $"Risk of numerical instability.");
        }
    }
}