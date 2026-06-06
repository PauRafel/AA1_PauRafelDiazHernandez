using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WavesUIController : MonoBehaviour
{
    [Header("References")]
    public WaveComparator waveComparator;
    public GerstnerWave gerstnerWave;
    public SinusoidalWave sinusoidalWave;

    [Header("Wave Parameter UI")]
    public Slider amplitudeSlider;
    public TMP_Text amplitudeLabel;
    public Slider frequencySlider;
    public TMP_Text frequencyLabel;
    public Slider wavelengthSlider;
    public TMP_Text wavelengthLabel;

    [Header("Control UI")]
    public Toggle gerstnerToggle;
    public TMP_Text activeModeLabel;
    public Button pauseButton;
    public TMP_Text pauseButtonText;
    public Button restartButton;

    private void Start()
    {
        amplitudeSlider.minValue = 0.1f;
        amplitudeSlider.maxValue = 2f;
        amplitudeSlider.value = 0.5f;

        frequencySlider.minValue = 0.1f;
        frequencySlider.maxValue = 5f;
        frequencySlider.value = 1f;

        wavelengthSlider.minValue = 0.5f;
        wavelengthSlider.maxValue = 8f;
        wavelengthSlider.value = 2f;

        amplitudeSlider.onValueChanged.AddListener(OnAmplitudeChanged);
        frequencySlider.onValueChanged.AddListener(OnFrequencyChanged);
        wavelengthSlider.onValueChanged.AddListener(OnWavelengthChanged);
        gerstnerToggle.onValueChanged.AddListener(OnToggleChanged);
        pauseButton.onClick.AddListener(OnPauseClicked);
        restartButton.onClick.AddListener(OnRestartClicked);

        UpdateModeLabel();
    }

    private void OnAmplitudeChanged(float value)
    {
        amplitudeLabel.text = $"Amplitude: {value:F2}";
        ApplyToAllWaves(w => w.amplitude = value);
    }

    private void OnFrequencyChanged(float value)
    {
        frequencyLabel.text = $"Frequency: {value:F2} Hz";
        ApplyToAllWaves(w => w.frequency = value);
    }

    private void OnWavelengthChanged(float value)
    {
        wavelengthLabel.text = $"Wavelength: {value:F2}";
        ApplyToAllWaves(w => w.wavelength = value);
    }

    private void ApplyToAllWaves(System.Action<WaveParameters> action)
    {
        if (gerstnerWave != null)
            foreach (var w in gerstnerWave.waves) action(w);

        if (sinusoidalWave != null)
            foreach (var w in sinusoidalWave.waves) action(w);
    }

    private void OnToggleChanged(bool isGerstner)
    {
        waveComparator.SetGerstnerMode(isGerstner);
        UpdateModeLabel();
    }

    private void UpdateModeLabel()
    {
        if (activeModeLabel != null)
            activeModeLabel.text = $"Mode: {waveComparator.GetActiveModeName()}";
    }

    private void OnPauseClicked()
    {
        SimulationManager.Instance.TogglePause();
        pauseButtonText.text = SimulationManager.Instance.IsPaused ? "Resume" : "Pause";
    }

    private void OnRestartClicked()
    {
        SimulationManager.Instance.RestartScene();
    }
}