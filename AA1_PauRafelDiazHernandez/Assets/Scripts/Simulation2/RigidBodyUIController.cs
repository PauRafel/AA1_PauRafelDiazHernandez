using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RigidBodyUIController : MonoBehaviour
{
    [Header("References")]
    public BallPhysics ball;
    public PhysicsManager physicsManager;

    [Header("UI Elements")]
    public Slider massSlider;
    public TMP_Text massLabel;
    public Slider airDensitySlider;
    public TMP_Text airDensityLabel;
    public Button pauseButton;
    public TMP_Text pauseButtonText;
    public Button restartButton;

    [Header("Data Display")]
    public TMP_Text velocityLabel;
    public TMP_Text forceLabel;
    public TMP_Text positionLabel;

    private void Start()
    {
        massSlider.minValue = 0.1f;
        massSlider.maxValue = 5f;
        massSlider.value = ball.mass;

        airDensitySlider.minValue = 0f;
        airDensitySlider.maxValue = 5f;
        airDensitySlider.value = physicsManager.airDensity;

        massSlider.onValueChanged.AddListener(OnMassChanged);
        airDensitySlider.onValueChanged.AddListener(OnAirDensityChanged);
        pauseButton.onClick.AddListener(OnPauseClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
    }

    private void Update()
    {
        if (ball == null) return;

        velocityLabel.text = $"Velocity: {ball.velocity.magnitude:F2} m/s";
        forceLabel.text = $"Angular Vel: {ball.angularVelocity.magnitude:F2} rad/s";
        positionLabel.text = $"Position: {ball.transform.position:F2}";
    }

    private void OnMassChanged(float value)
    {
        ball.mass = value;
        massLabel.text = $"Mass: {value:F1} kg";
    }

    private void OnAirDensityChanged(float value)
    {
        physicsManager.airDensity = value;
        airDensityLabel.text = $"Air Density: {value:F2}";
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