using UnityEngine;
using UnityEngine.InputSystem;

public class FreeCameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float fastMultiplier = 3f;
    public float mouseSensitivity = 0.2f;

    [Header("Zoom Settings")]
    public float scrollSpeed = 5f;

    private float _yaw = 0f;
    private float _pitch = 0f;
    private bool _isRightMouseHeld = false;

    private void Start()
    {
        _yaw = transform.eulerAngles.y;
        _pitch = transform.eulerAngles.x;
    }

    private void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleScroll();
    }

    private void HandleRotation()
    {
        _isRightMouseHeld = Mouse.current.rightButton.isPressed;

        if (!_isRightMouseHeld)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        _yaw += mouseDelta.x * mouseSensitivity;
        _pitch -= mouseDelta.y * mouseSensitivity;
        _pitch = Mathf.Clamp(_pitch, -89f, 89f);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private void HandleMovement()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float speed = moveSpeed;
        if (keyboard.leftShiftKey.isPressed)
            speed *= fastMultiplier;

        Vector3 dir = Vector3.zero;

        if (keyboard.wKey.isPressed) dir += transform.forward;
        if (keyboard.sKey.isPressed) dir -= transform.forward;
        if (keyboard.aKey.isPressed) dir -= transform.right;
        if (keyboard.dKey.isPressed) dir += transform.right;
        if (keyboard.eKey.isPressed) dir += transform.up;
        if (keyboard.qKey.isPressed) dir -= transform.up;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void HandleScroll()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (Mathf.Abs(scroll) > 0.01f)
            transform.position += transform.forward * scroll * scrollSpeed * Time.deltaTime;
    }
}