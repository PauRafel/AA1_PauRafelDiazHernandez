using UnityEngine;

public class GraphColorConfig : MonoBehaviour
{
    [Header("References")]
    public GraphRenderer graphRenderer;

    [Header("Color Presets")]
    public Color velocityColor = Color.green;
    public Color forceColor = Color.red;
    public Color displacementColor = Color.cyan;

    private SimulationGraphPanel _graphPanel;

    private void Awake()
    {
        _graphPanel = GetComponent<SimulationGraphPanel>();
    }

    private void Update()
    {
        if (_graphPanel == null || graphRenderer == null) return;

        switch (_graphPanel.currentMode)
        {
            case SimulationGraphPanel.GraphMode.Velocity:
                graphRenderer.lineColor = velocityColor;
                break;
            case SimulationGraphPanel.GraphMode.Force:
                graphRenderer.lineColor = forceColor;
                break;
            case SimulationGraphPanel.GraphMode.Displacement:
                graphRenderer.lineColor = displacementColor;
                break;
        }
    }
}