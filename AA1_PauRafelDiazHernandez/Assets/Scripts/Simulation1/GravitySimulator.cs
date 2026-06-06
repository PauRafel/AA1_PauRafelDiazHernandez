using UnityEngine;

public class GravitySimulator : MonoBehaviour
{
    [Header("Config")]
    public MagicNumbersConfig physicsConfig;

    [Header("Bodies")]
    public CelestialBody[] bodies;

    private float G => physicsConfig != null ?
        physicsConfig.gravitationalConstant : 39.478f;

    private void FixedUpdate()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        CalculateForces();
        UpdatePositions();
    }

    private void CalculateForces()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            for (int j = i + 1; j < bodies.Length; j++)
            {
                CelestialBody bodyA = bodies[i];
                CelestialBody bodyB = bodies[j];

                Vector3 direction = bodyB.transform.position - bodyA.transform.position;
                float distance = Mathf.Max(direction.magnitude, 0.1f);

                float forceMagnitude = G * (bodyA.mass * bodyB.mass)
                                       / (distance * distance);

                Vector3 force = direction.normalized * forceMagnitude;

                bodyA.AddForce(force);
                bodyB.AddForce(-force);
            }
        }
    }

    private void UpdatePositions()
    {
        foreach (var body in bodies)
            body.UpdatePosition(Time.fixedDeltaTime);
    }
}