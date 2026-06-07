using UnityEngine;
using TMPro;

public class SceneTitleDisplay : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text sceneTitleLabel;
    public TMP_Text sceneSubtitleLabel;

    [Header("Scene Info")]
    public string sceneTitle = "Simulación";
    public string sceneSubtitle = "Mecánica - ENTI Barcelona";

    private void Start()
    {
        if (sceneTitleLabel != null)
            sceneTitleLabel.text = sceneTitle;

        if (sceneSubtitleLabel != null)
            sceneSubtitleLabel.text = sceneSubtitle;
    }
}