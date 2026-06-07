using UnityEngine;

public class OrbitTrail : MonoBehaviour, IResettable
{
    [Header("Trail Settings")]
    public float trailTime = 5f;
    public float startWidth = 0.05f;
    public float endWidth = 0f;
    public Color trailColor = Color.white;

    private TrailRenderer _trail;

    private void Awake()
    {
        _trail = gameObject.AddComponent<TrailRenderer>();
        _trail.time = trailTime;
        _trail.startWidth = startWidth;
        _trail.endWidth = endWidth;
        _trail.material = new Material(Shader.Find("Sprites/Default"));
        _trail.startColor = trailColor;
        _trail.endColor = new Color(trailColor.r, trailColor.g, trailColor.b, 0f);
    }

    public void ResetSimulation()
    {
        _trail.Clear();
    }
}