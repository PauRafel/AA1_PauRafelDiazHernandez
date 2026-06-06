using UnityEngine;
using UnityEngine.Events;

public class WaveComparator : MonoBehaviour, IResettable
{
    [Header("References")]
    public GerstnerWave gerstnerWave;
    public SinusoidalWave sinusoidalWave;
    public BuoyancyObject buoy;

    [Header("State")]
    public bool isGerstnerActive = true;

    [Header("Events")]
    public UnityEvent<bool> onWaveModeChanged;

    private void Start()
    {
        ApplyWaveMode();
    }

    public void ToggleWaveMode()
    {
        isGerstnerActive = !isGerstnerActive;
        ApplyWaveMode();
        onWaveModeChanged?.Invoke(isGerstnerActive);
    }

    public void SetGerstnerMode(bool useGerstner)
    {
        isGerstnerActive = useGerstner;
        ApplyWaveMode();
        onWaveModeChanged?.Invoke(isGerstnerActive);
    }

    private void ApplyWaveMode()
    {
        if (gerstnerWave != null)
            gerstnerWave.gameObject.SetActive(isGerstnerActive);

        if (sinusoidalWave != null)
            sinusoidalWave.gameObject.SetActive(!isGerstnerActive);

        if (buoy != null)
            buoy.useGerstner = isGerstnerActive;
    }

    public string GetActiveModeName()
    {
        return isGerstnerActive ? "Gerstner" : "Sinusoidal";
    }

    public void ResetSimulation()
    {
        isGerstnerActive = true;
        ApplyWaveMode();
    }
}