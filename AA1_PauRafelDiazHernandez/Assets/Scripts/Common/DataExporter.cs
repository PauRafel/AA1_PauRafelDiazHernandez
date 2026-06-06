using UnityEngine;
using System.IO;
using System.Text;
using TMPro;

public class DataExporter : MonoBehaviour
{
    [Header("References")]
    public PhysicsDataRecorder dataRecorder;

    [Header("UI")]
    public TMP_Text exportStatusLabel;

    [Header("Settings")]
    public string fileName = "simulation_data";

    public void ExportToCSV()
    {
        if (dataRecorder == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Sample,Velocity,Force,Displacement");

        var velocities = dataRecorder.GetVelocitySamples();
        var forces = dataRecorder.GetForceSamples();
        var displacements = dataRecorder.GetDisplacementSamples();

        int count = Mathf.Min(velocities.Count,
                    Mathf.Min(forces.Count, displacements.Count));

        for (int i = 0; i < count; i++)
        {
            sb.AppendLine($"{i}," +
                          $"{velocities[i]:F4}," +
                          $"{forces[i]:F4}," +
                          $"{displacements[i]:F4}");
        }

        string path = Path.Combine(Application.persistentDataPath,
                                   $"{fileName}_{System.DateTime.Now:yyyyMMdd_HHmmss}.csv");
        File.WriteAllText(path, sb.ToString());

        Debug.Log($"[DataExporter] Exported to: {path}");

        if (exportStatusLabel != null)
            exportStatusLabel.text = $"Exported: {Path.GetFileName(path)}";
    }
}