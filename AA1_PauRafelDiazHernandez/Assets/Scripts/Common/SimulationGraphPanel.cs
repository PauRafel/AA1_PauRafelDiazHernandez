using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationGraphPanel : MonoBehaviour
{
    public enum GraphMode
    {
        Velocity,
        Force,
        Displacement
    }

    [Header("References")]
    public PhysicsDataRecorder dataRecorder;
    public GraphRenderer graphRenderer;

    [Header("UI Elements")]
    public TMP_Text graphTitleLabel;
    public Button velocityButton;
    public Button forceButton;
    public Button displacementButton;

    [Header("Settings")]
    public GraphMode currentMode = GraphMode.Velocity;
    public float updateInterval = 0.05f;

    private float _timer = 0f;

    private void Start()
    {
        velocityButton.onClick.AddListener(() => SetMode(GraphMode.Velocity));
        forceButton.onClick.AddListener(() => SetMode(GraphMode.Force));
        displacementButton.onClick.AddListener(() => SetMode(GraphMode.Displacement));

        UpdateTitle();
    }

    private void Update()
    {
        if (dataRecorder == null || graphRenderer == null) return;

        _timer += Time.deltaTime;
        if (_timer < updateInterval) return;
        _timer = 0f;

        RefreshGraph();
    }

    private void RefreshGraph()
    {
        switch (currentMode)
        {
            case GraphMode.Velocity:
                graphRenderer.DrawGraph(dataRecorder.GetVelocitySamples());
                break;
            case GraphMode.Force:
                graphRenderer.DrawGraph(dataRecorder.GetForceSamples());
                break;
            case GraphMode.Displacement:
                graphRenderer.DrawGraph(dataRecorder.GetDisplacementSamples());
                break;
        }
    }

    public void SetMode(GraphMode mode)
    {
        currentMode = mode;
        UpdateTitle();
    }

    private void UpdateTitle()
    {
        if (graphTitleLabel != null)
            graphTitleLabel.text = $"Graph: {currentMode}";
    }
}