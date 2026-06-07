using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewtonianUIController : MonoBehaviour
{
    [Header("References")]
    public GravitySimulator gravitySimulator;
    public TimeScaleController timeScaleController;

    [Header("UI Elements")]
    public Slider timeScaleSlider;
    public TMP_Text timeScaleLabel;
    public Button pauseButton;
    public TMP_Text pauseButtonText;
    public Button restartButton;

    private void Start()
    {
        if (timeScaleController == null)
            timeScaleController = FindFirstObjectByType<TimeScaleController>();

        timeScaleSlider.minValue = 0.1f;
        timeScaleSlider.maxValue = 10f;
        timeScaleSlider.value = 1f;

        timeScaleSlider.onValueChanged.AddListener(OnTimeScaleChanged);
        pauseButton.onClick.AddListener(OnPauseClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
    }

    private void OnTimeScaleChanged(float value)
    {
        timeScaleController.SetTimeScale(value);
        timeScaleLabel.text = $"Time Scale: {value:F1}x";
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

    private void Update()
    {
        if (timeScaleLabel != null)
            timeScaleLabel.text = $"Time Scale: {timeScaleController.timeScale:F1}x";
    }
}