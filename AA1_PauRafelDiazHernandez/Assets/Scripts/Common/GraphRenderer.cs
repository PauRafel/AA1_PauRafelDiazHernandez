using UnityEngine;
using UnityEngine.UI;

public class GraphRenderer : MonoBehaviour
{
    [Header("Graph Settings")]
    public int width = 256;
    public int height = 128;
    public Color backgroundColor = new Color(0.1f, 0.1f, 0.1f);
    public Color lineColor = Color.green;
    public Color gridColor = new Color(0.3f, 0.3f, 0.3f);

    private Texture2D _texture;
    private RawImage _rawImage;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
        _texture = new Texture2D(width, height);
        _texture.filterMode = FilterMode.Point;
        _rawImage.texture = _texture;
        ClearGraph();
    }

    public void DrawGraph(System.Collections.Generic.List<float> samples)
    {
        ClearGraph();
        DrawGrid();

        if (samples == null || samples.Count < 2) return;

        float maxVal = Mathf.Max(0.001f, GetMax(samples));

        for (int i = 1; i < samples.Count; i++)
        {
            int x0 = Mathf.RoundToInt((i - 1) / (float)samples.Count * width);
            int x1 = Mathf.RoundToInt(i / (float)samples.Count * width);
            int y0 = Mathf.RoundToInt(samples[i - 1] / maxVal * (height - 4));
            int y1 = Mathf.RoundToInt(samples[i] / maxVal * (height - 4));

            DrawLine(x0, y0, x1, y1, lineColor);
        }

        _texture.Apply();
    }

    private void DrawGrid()
    {
        for (int x = 0; x < width; x += width / 4)
            for (int y = 0; y < height; y++)
                _texture.SetPixel(x, y, gridColor);

        for (int y = 0; y < height; y += height / 4)
            for (int x = 0; x < width; x++)
                _texture.SetPixel(x, y, gridColor);
    }

    private void DrawLine(int x0, int y0, int x1, int y1, Color color)
    {
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height)
                _texture.SetPixel(x0, y0, color);

            if (x0 == x1 && y0 == y1) break;

            int e2 = 2 * err;
            if (e2 > -dy) { err -= dy; x0 += sx; }
            if (e2 < dx) { err += dx; y0 += sy; }
        }
    }

    private void ClearGraph()
    {
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = backgroundColor;
        _texture.SetPixels(pixels);
    }

    private float GetMax(System.Collections.Generic.List<float> samples)
    {
        float max = float.MinValue;
        foreach (var s in samples)
            if (s > max) max = s;
        return max;
    }
}