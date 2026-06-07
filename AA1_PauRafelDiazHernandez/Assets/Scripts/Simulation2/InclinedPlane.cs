using UnityEngine;

public class InclinedPlane : MonoBehaviour
{
    [Header("Ramp Properties")]
    [Range(0f, 90f)]
    public float angleInDegrees = 30f;

    private float AngleInRadians => angleInDegrees * Mathf.Deg2Rad;

    public float GetParallelForce(float mass)
    {
        return mass * Mathf.Abs(Physics.gravity.y) * Mathf.Sin(AngleInRadians);
    }

    public float GetNormalForce(float mass)
    {
        return mass * Mathf.Abs(Physics.gravity.y) * Mathf.Cos(AngleInRadians);
    }

    public Vector3 GetSlopeDirection()
    {
        return new Vector3(Mathf.Cos(AngleInRadians), -Mathf.Sin(AngleInRadians), 0f).normalized;
    }

    private void OnTriggerStay(Collider other)
    {
        BallPhysics ball = other.GetComponent<BallPhysics>();
        if (ball == null) return;

        float parallelForce = GetParallelForce(ball.mass);
        Vector3 slopeForce = GetSlopeDirection() * parallelForce;

        ball.ApplyForce(slopeForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,
            transform.position + GetSlopeDirection() * 2f);
    }
}