using UnityEngine;

public class PhysicsManager : MonoBehaviour, IResettable
{
    [Header("References")]
    public BallPhysics ball;
    public CollisionHandler collisionHandler;

    [Header("Config")]
    public MagicNumbersConfig physicsConfig;

    [Header("Air Resistance Settings")]
    public float airDensity = 1.225f;
    public float dragCoefficient = 0.47f;
    public float ballCrossSection = 0.0079f;

    [Header("Ground Settings")]
    public float groundRestitution = 0.5f;

    [Header("Collision Settings")]
    public LayerMask collisionLayers;

    private void FixedUpdate()
    {
        if (SimulationManager.Instance != null && SimulationManager.Instance.IsPaused)
            return;

        if (ball == null) return;

        ball.ApplyGravity();
        ball.ApplyAirResistance(airDensity, dragCoefficient, ballCrossSection);
        HandleMovementWithCollision();
    }

    private void HandleMovementWithCollision()
    {
        Vector3 movement = ball.velocity * Time.fixedDeltaTime;
        float moveDist = movement.magnitude;

        if (moveDist < 0.0001f)
        {
            ResolveOverlaps();
            return;
        }

        if (Physics.SphereCast(
            ball.transform.position,
            ball.radius * 1.00f,
            movement.normalized,
            out RaycastHit hit,
            moveDist,
            collisionLayers))
        {
            float safeDistance = Mathf.Max(0f, hit.distance - 0.001f);
            ball.transform.position += movement.normalized * safeDistance;

            float restitution = groundRestitution;
            ObstacleProperties props = hit.collider.GetComponent<ObstacleProperties>();
            if (props != null && physicsConfig != null)
            {
                restitution = props.isElastic ?
                    physicsConfig.elasticRestitution :
                    physicsConfig.inelasticRestitution;
            }

            float normalSpeed = Vector3.Dot(ball.velocity, hit.normal);
            Vector3 normalVelocity = hit.normal * normalSpeed;
            Vector3 tangentialVelocity = ball.velocity - normalVelocity;


            if (normalSpeed < 0f)
            {
     
                ball.velocity = tangentialVelocity + (-normalVelocity * restitution);
            }

            TerrainZone zone = hit.collider.GetComponent<TerrainZone>();
            if (zone != null)
                ball.ApplyFriction(zone.FrictionCoefficient);

            float remainingTime = Time.fixedDeltaTime - (safeDistance / moveDist) * Time.fixedDeltaTime;
            ball.transform.position += ball.velocity * remainingTime;
        }
        else
        {
            ball.transform.position += movement;
        }
    }

    private void ResolveOverlaps()
    {
        Collider[] overlaps = Physics.OverlapSphere(
            ball.transform.position,
            ball.radius,
            collisionLayers
        );

        foreach (var col in overlaps)
        {
            if (col.gameObject == ball.gameObject) continue;

            Vector3 direction;
            float distance;

            if (Physics.ComputePenetration(
                ball.GetComponent<Collider>(),
                ball.transform.position,
                ball.transform.rotation,
                col,
                col.transform.position,
                col.transform.rotation,
                out direction,
                out distance))
            {
                ball.transform.position += direction * (distance + 0.001f);
                ball.velocity = Vector3.Reflect(ball.velocity, direction) * groundRestitution;
            }
        }
    }

    public void SetCurrentZone(TerrainZone zone) { }

    public void ResetSimulation()
    {
        ball.ResetSimulation();
    }
}