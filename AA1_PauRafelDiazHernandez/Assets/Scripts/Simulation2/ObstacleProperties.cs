using UnityEngine;

public class ObstacleProperties : MonoBehaviour
{
    [Header("Collision Properties")]
    public bool isElastic = true;

    public string Description =>
        isElastic ? "Elastic (e=0.8)" : "Inelastic (e=0.2)";
}