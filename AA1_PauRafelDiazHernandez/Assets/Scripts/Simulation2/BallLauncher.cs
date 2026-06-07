using UnityEngine;
using UnityEngine.InputSystem;

public class BallLauncher : MonoBehaviour
{
    [Header("References")]
    public BallPhysics ball;
    public LineRenderer trajectoryLine;

    [Header("Launch Settings")]
    [Range(1f, 50f)]
    public float maxLaunchForce = 15f;
    [Range(0.01f, 1f)]
    public float sensitivity = 0.1f;
    public int trajectorySteps = 40;
    public float trajectoryTimeStep = 0.05f;

    private Vector3 _dragStartWorld;
    private bool _isDragging = false;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        SetupTrajectoryLine();
    }

    private void SetupTrajectoryLine()
    {
        if (trajectoryLine == null)
            trajectoryLine = gameObject.AddComponent<LineRenderer>();

        trajectoryLine.startWidth = 0.08f;
        trajectoryLine.endWidth = 0.02f;
        trajectoryLine.positionCount = trajectorySteps;
        trajectoryLine.enabled = false;
        trajectoryLine.material = new Material(Shader.Find("Sprites/Default"));
        trajectoryLine.material.color = Color.yellow;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            TryStartDrag();

        if (_isDragging && Mouse.current.leftButton.isPressed)
            UpdateDrag();

        if (_isDragging && Mouse.current.leftButton.wasReleasedThisFrame)
            ReleaseDrag();
    }

    private void TryStartDrag()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(screenPos);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            if (hit.collider.gameObject == ball.gameObject)
            {
                _isDragging = true;
                _dragStartWorld = ball.transform.position;
            }
        }
    }

    private void UpdateDrag()
    {
        Vector3 force = GetCurrentForce();
        trajectoryLine.enabled = true;
        DrawTrajectory(force);
    }

    private void ReleaseDrag()
    {
        Vector3 force = GetCurrentForce();
        trajectoryLine.enabled = false;
        _isDragging = false;

        ball.velocity = Vector3.zero;
        ball.ApplyForce(force);
    }

    private Vector3 GetCurrentForce()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        Vector3 ballViewport = _camera.WorldToViewportPoint(ball.transform.position);
        Vector3 mouseWorld = _camera.ViewportToWorldPoint(new Vector3(
            screenPos.x / Screen.width,
            screenPos.y / Screen.height,
            ballViewport.z
        ));

        Vector3 delta = _dragStartWorld - mouseWorld;
        delta.y = 0f;
        delta = Vector3.ClampMagnitude(delta, maxLaunchForce);

        Vector3 force = delta * (maxLaunchForce * sensitivity);
        force.y = force.magnitude * 1.0f;

        return force;
    }

    private void DrawTrajectory(Vector3 force)
    {
        Vector3 pos = ball.transform.position;
        Vector3 vel = force / ball.mass;

        for (int i = 0; i < trajectorySteps; i++)
        {
            trajectoryLine.SetPosition(i, pos);
            vel.y += Physics.gravity.y * trajectoryTimeStep;
            pos += vel * trajectoryTimeStep;
        }
    }

    public void ResetLauncher()
    {
        _isDragging = false;
        trajectoryLine.enabled = false;
    }
}