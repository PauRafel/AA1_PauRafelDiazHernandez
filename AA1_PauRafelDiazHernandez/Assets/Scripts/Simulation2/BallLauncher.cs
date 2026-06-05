using UnityEngine;
using UnityEngine.InputSystem;

public class BallLauncher : MonoBehaviour
{
    [Header("References")]
    public BallPhysics ball;
    public LineRenderer trajectoryLine;

    [Header("Launch Settings")]
    public float maxLaunchForce = 20f;
    public int trajectorySteps = 30;
    public float trajectoryTimeStep = 0.05f;

    private Vector2 _dragStart;
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

        trajectoryLine.startWidth = 0.05f;
        trajectoryLine.endWidth = 0.02f;
        trajectoryLine.positionCount = trajectorySteps;
        trajectoryLine.enabled = false;

        Material mat = new Material(Shader.Find("Sprites/Default"));
        mat.color = Color.yellow;
        trajectoryLine.material = mat;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            OnDragStart();
        else if (Mouse.current.leftButton.isPressed && _isDragging)
            OnDragging();
        else if (Mouse.current.leftButton.wasReleasedThisFrame && _isDragging)
            OnDragRelease();
    }

    private void OnDragStart()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<BallPhysics>() != null)
            {
                _dragStart = mousePos;
                _isDragging = true;
            }
        }
    }

    private void OnDragging()
    {
        Vector3 launchForce = CalculateLaunchForce();
        DrawTrajectory(launchForce);
        trajectoryLine.enabled = true;
    }

    private void OnDragRelease()
    {
        Vector3 launchForce = CalculateLaunchForce();
        ball.ApplyForce(launchForce);
        trajectoryLine.enabled = false;
        _isDragging = false;
    }

    private Vector3 CalculateLaunchForce()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 dragDelta = _dragStart - mousePos;
        dragDelta = Vector2.ClampMagnitude(dragDelta, maxLaunchForce);
        return new Vector3(dragDelta.x, 0f, dragDelta.y);
    }

    private void DrawTrajectory(Vector3 force)
    {
        Vector3 pos = ball.transform.position;
        Vector3 vel = ball.velocity + (force / ball.mass) * trajectoryTimeStep;

        for (int i = 0; i < trajectorySteps; i++)
        {
            trajectoryLine.SetPosition(i, pos);
            vel.y += Physics.gravity.y * trajectoryTimeStep;
            pos += vel * trajectoryTimeStep;
        }
    }
}