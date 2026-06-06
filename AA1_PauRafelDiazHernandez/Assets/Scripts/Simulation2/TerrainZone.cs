using UnityEngine;

public enum TerrainType
{
    Grass,
    Ice,
    Sand
}

public class TerrainZone : MonoBehaviour
{
    [Header("Terrain Properties")]
    public TerrainType terrainType = TerrainType.Grass;

    public float FrictionCoefficient
    {
        get
        {
            switch (terrainType)
            {
                case TerrainType.Grass: return 0.4f;
                case TerrainType.Ice: return 0.1f;
                case TerrainType.Sand: return 0.6f;
                default: return 0.4f;
            }
        }
    }

    public Color ZoneColor
    {
        get
        {
            switch (terrainType)
            {
                case TerrainType.Grass: return new Color(0.2f, 0.6f, 0.2f);
                case TerrainType.Ice: return new Color(0.6f, 0.9f, 1.0f);
                case TerrainType.Sand: return new Color(0.9f, 0.8f, 0.4f);
                default: return Color.white;
            }
        }
    }

    private void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            mat.color = ZoneColor;
            rend.material = mat;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        BallPhysics ball = other.GetComponent<BallPhysics>();
        if (ball != null)
            ball.ApplyFriction(FrictionCoefficient);
    }
}