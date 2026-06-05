using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [Header("Restitution Coefficients")]
    public float elasticRestitution = 0.8f;
    public float inelasticRestitution = 0.2f;

    private BallPhysics _ball;

    private void Awake()
    {
        _ball = GetComponent<BallPhysics>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        HandleCollision(hit.normal, hit.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        HandleCollision(normal, collision.gameObject);
    }

    private void HandleCollision(Vector3 normal, GameObject other)
    {
        if (_ball == null) return;

        float restitution = GetRestitution(other);

        Vector3 reflected = Vector3.Reflect(_ball.velocity, normal);
        _ball.velocity = reflected * restitution;
    }

    private float GetRestitution(GameObject other)
    {
        ObstacleProperties props = other.GetComponent<ObstacleProperties>();
        if (props != null)
            return props.isElastic ? elasticRestitution : inelasticRestitution;

        return elasticRestitution;
    }
}