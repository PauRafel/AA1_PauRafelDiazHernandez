using UnityEngine;
using TMPro;

public class NewtonianDataDisplay : MonoBehaviour
{
    [Header("References")]
    public CelestialBody selectedBody;

    [Header("UI Elements")]
    public TMP_Text bodyNameLabel;
    public TMP_Text velocityLabel;
    public TMP_Text forceLabel;
    public TMP_Text distanceLabel;

    private Vector3 _sunPosition;

    private void Start()
    {
        GameObject sun = GameObject.Find("Sun");
        if (sun != null)
            _sunPosition = sun.transform.position;
    }

    private void Update()
    {
        if (selectedBody == null) return;

        float velocity = selectedBody.velocity.magnitude;
        float force = selectedBody.currentForce.magnitude;
        float distance = Vector3.Distance(selectedBody.transform.position, _sunPosition);

        bodyNameLabel.text = $"Body: {selectedBody.name}";
        velocityLabel.text = $"Velocity: {velocity:F3} UA/year";
        forceLabel.text = $"Force: {force:F4} N";
        distanceLabel.text = $"Distance to Sun: {distance:F3} UA";
    }

    public void SetSelectedBody(CelestialBody body)
    {
        selectedBody = body;
    }
}