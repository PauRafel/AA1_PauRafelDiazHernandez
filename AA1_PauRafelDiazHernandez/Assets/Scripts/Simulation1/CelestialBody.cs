using UnityEngine;

public class CelestialBody : MonoBehaviour, IResettable
{
    [Header("Physical Properties")]
    public float mass = 1f;
    public Vector3 initialVelocity = Vector3.zero;

    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Vector3 currentForce;

    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
        velocity = initialVelocity;
    }

    public void AddForce(Vector3 force)
    {
        currentForce += force;
    }

    public void UpdatePosition(float deltaTime)
    {
        Vector3 acceleration = currentForce / mass;
        velocity += acceleration * deltaTime;
        transform.position += velocity * deltaTime;
        currentForce = Vector3.zero;
    }

    public void ResetSimulation()
    {
        transform.position = _initialPosition;
        velocity = initialVelocity;
        currentForce = Vector3.zero;
    }
}