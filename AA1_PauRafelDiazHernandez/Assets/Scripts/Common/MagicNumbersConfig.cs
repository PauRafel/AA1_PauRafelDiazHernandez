using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsConfig", menuName = "Simulation/Physics Config")]
public class MagicNumbersConfig : ScriptableObject
{
    [Header("Gravity")]
    public float gravitationalConstant = 39.478f;
    public float earthGravity = 9.81f;

    [Header("Terrain Friction")]
    public float grassFriction = 0.4f;
    public float iceFriction = 0.1f;
    public float sandFriction = 0.6f;

    [Header("Collisions")]
    public float elasticRestitution = 0.8f;
    public float inelasticRestitution = 0.2f;

    [Header("Air Resistance")]
    public float airDensity = 1.225f;
    public float dragCoefficient = 0.47f;
    public float ballCrossSection = 0.0079f;

    [Header("Buoyancy")]
    public float waterDensity = 1000f;

    [Header("Wave Limits")]
    public float maxWaveAmplitude = 5f;
    public float minWavelength = 0.5f;
    public float maxFrequency = 10f;
}