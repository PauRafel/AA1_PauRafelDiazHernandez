using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float timeScale = 1f;

    private float _previousTimeScale = 1f;

    private void Update()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        if (!Mathf.Approximately(timeScale, _previousTimeScale))
        {
            Time.timeScale = timeScale;
            _previousTimeScale = timeScale;
        }
    }

    public void SetTimeScale(float value)
    {
        timeScale = Mathf.Clamp(value, 0.1f, 10f);
    }

    public void ResetTimeScale()
    {
        timeScale = 1f;
    }
}