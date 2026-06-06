using UnityEngine;

public class BallPhysics : MonoBehaviour, IResettable
{
    [Header("Ball Properties")]
    public float mass = 1f;
    public float radius = 0.5f;

    [Header("State")]
    public Vector3 velocity = Vector3.zero;
    public Vector3 angularVelocity = Vector3.zero;

    private Vector3 _initialPosition;
    private Vector3 _initialVelocity;
    private const float Gravity = -9.81f;

    private void Start()
    {
        _initialPosition = transform.position;
        _initialVelocity = velocity;
    }

    public void ApplyForce(Vector3 force)
    {
        Vector3 acceleration = force / mass;
        velocity += acceleration * Time.fixedDeltaTime;
    }

    public void ApplyGravity()
    {
        velocity.y += Gravity * Time.fixedDeltaTime;
    }

    public void ApplyFriction(float frictionCoefficient)
    {
        if (velocity.magnitude < 0.01f)
        {
            velocity = Vector3.zero;
            return;
        }

        float normalForce = mass * Mathf.Abs(Gravity);
        float frictionMagnitude = frictionCoefficient * normalForce;
        Vector3 frictionForce = -velocity.normalized * frictionMagnitude;

        ApplyForce(frictionForce);
    }

    public void ApplyAirResistance(float density, float dragCoefficient, float area)
    {
        if (transform.position.y < 1f) return;

        float speedSq = velocity.sqrMagnitude;
        float magnitude = 0.5f * density * speedSq * dragCoefficient * area;
        Vector3 drag = -velocity.normalized * magnitude;

        ApplyForce(drag);
    }

    public void UpdateAngularVelocity()
    {
        if (radius > 0f)
            angularVelocity = velocity / radius;
    }

    public void Move()
    {
        transform.position += velocity * Time.fixedDeltaTime;
        UpdateAngularVelocity();
    }

    public void ResetSimulation()
    {
        transform.position = _initialPosition;
        velocity = _initialVelocity;
        angularVelocity = Vector3.zero;
    }
}