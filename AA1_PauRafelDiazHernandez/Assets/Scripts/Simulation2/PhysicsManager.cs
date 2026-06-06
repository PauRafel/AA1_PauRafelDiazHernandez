using UnityEngine;

public class PhysicsManager : MonoBehaviour, IResettable
{
    [Header("References")]
    public BallPhysics ball;
    public CollisionHandler collisionHandler;

    [Header("Air Resistance Settings")]
    public float airDensity = 1.225f;
    public float dragCoefficient = 0.47f;
    public float ballCrossSection = 0.0079f;

    [Header("Ground Settings")]
    public float groundY = 0f;
    public float groundRestitution = 0.5f;

    private TerrainZone _currentZone;

    private void FixedUpdate()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        if (ball == null) return;

        ball.ApplyGravity();
        ball.ApplyAirResistance(airDensity, dragCoefficient, ballCrossSection);
        HandleGroundCollision();
        ball.Move();
    }

    private void HandleGroundCollision()
    {
        if (ball.transform.position.y - ball.radius <= groundY)
        {
            Vector3 pos = ball.transform.position;
            pos.y = groundY + ball.radius;
            ball.transform.position = pos;

            if (ball.velocity.y < 0f)
                ball.velocity.y = -ball.velocity.y * groundRestitution;
        }
    }

    public void SetCurrentZone(TerrainZone zone)
    {
        _currentZone = zone;
    }

    public void ResetSimulation()
    {
        ball.ResetSimulation();
    }
}