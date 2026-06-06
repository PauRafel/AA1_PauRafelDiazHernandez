using UnityEngine;
using TMPro;

public class WaveDataDisplay : MonoBehaviour
{
    [Header("References")]
    public BuoyancyObject buoy;
    public WaveComparator waveComparator;

    [Header("UI Elements")]
    public TMP_Text buoyHeightLabel;
    public TMP_Text buoyancyForceLabel;
    public TMP_Text waveModeLabel;
    public TMP_Text waveHeightLabel;

    private float _previousHeight;

    private void Update()
    {
        if (buoy == null || waveComparator == null) return;

        UpdateBuoyData();
        UpdateWaveData();
    }

    private void UpdateBuoyData()
    {
        float currentHeight = buoy.transform.position.y;

        float waterDensity = buoy.physicsConfig != null ?
            buoy.physicsConfig.waterDensity : 1000f;
        float gravity = buoy.physicsConfig != null ?
            buoy.physicsConfig.earthGravity : 9.81f;

        float displacement = Mathf.Max(0f, currentHeight - _previousHeight);
        float buoyancyForce = waterDensity * gravity * buoy.objectVolume * displacement;

        buoyHeightLabel.text = $"Buoy Height: {currentHeight:F3} m";
        buoyancyForceLabel.text = $"Buoyancy Force: {buoyancyForce:F3} N";

        _previousHeight = currentHeight;
    }

    private void UpdateWaveData()
    {
        float x = buoy.transform.position.x;
        float z = buoy.transform.position.z;
        float waterHeight = 0f;

        if (waveComparator.isGerstnerActive && waveComparator.gerstnerWave != null)
            waterHeight = waveComparator.gerstnerWave.GetHeightAt(x, z);
        else if (waveComparator.sinusoidalWave != null)
            waterHeight = waveComparator.sinusoidalWave.GetHeightAt(x, z);

        waveModeLabel.text = $"Mode: {waveComparator.GetActiveModeName()}";
        waveHeightLabel.text = $"Wave Height: {waterHeight:F3} m";
    }
}