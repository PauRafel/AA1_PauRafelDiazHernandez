using UnityEngine;

public class OrbitStabilityChecker : MonoBehaviour
{
    [Header("References")]
    public CelestialBody[] bodiesToCheck;

    [Header("Settings")]
    public float maxDistanceFromSun = 50f;
    public float checkInterval = 2f;

    private float _timer = 0f;
    private Transform _sun;

    private void Start()
    {
        GameObject sun = GameObject.Find("Sun");
        if (sun != null)
            _sun = sun.transform;
    }

    private void Update()
    {
        if (_sun == null) return;

        _timer += Time.deltaTime;
        if (_timer < checkInterval) return;
        _timer = 0f;

        foreach (var body in bodiesToCheck)
        {
            if (body == null) continue;

            float distance = Vector3.Distance(body.transform.position, _sun.position);

            if (distance > maxDistanceFromSun)
            {
                Debug.LogWarning($"[OrbitStability] {body.name} has escaped orbit! " +
                                 $"Distance: {distance:F2} UA");
            }
        }
    }
}