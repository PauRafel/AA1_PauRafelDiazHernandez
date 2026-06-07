using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("References")]
    public SceneLoader sceneLoader;

    [Header("Buttons")]
    public Button buttonNewtonian;
    public Button buttonRigidBody;
    public Button buttonWaves;
    public Button buttonQuit;

    [Header("Description Panel")]
    public TMP_Text descriptionLabel;

    private void Start()
    {
        buttonNewtonian.onClick.AddListener(sceneLoader.LoadNewtonian);
        buttonRigidBody.onClick.AddListener(sceneLoader.LoadRigidBody);
        buttonWaves.onClick.AddListener(sceneLoader.LoadWaves);
        buttonQuit.onClick.AddListener(OnQuit);

        descriptionLabel.text = "Selecciona una simulación para comenzar.";
    }

    private void OnQuit()
    {
        Application.Quit();
        Debug.Log("[MainMenu] Quit application");
    }
}