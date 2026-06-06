using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text fpsLabel;

    [Header("Settings")]
    public float updateInterval = 0.5f;

    private float _timer = 0f;
    private int _frameCount = 0;
    private float _fps = 0f;

    private void Update()
    {
        _frameCount++;
        _timer += Time.unscaledDeltaTime;

        if (_timer < updateInterval) return;

        _fps = _frameCount / _timer;
        _frameCount = 0;
        _timer = 0f;

        if (fpsLabel != null)
            fpsLabel.text = $"FPS: {_fps:F0}";
    }
}