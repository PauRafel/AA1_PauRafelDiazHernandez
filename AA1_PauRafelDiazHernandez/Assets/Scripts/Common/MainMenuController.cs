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

    private const string DESC_NEWTONIAN =
        "Simulación 1: Miniuniverso Newtoniano\n\n" +
        "Sistema de 3-5 cuerpos celestes en órbita.\n" +
        "Gravedad newtoniana, trayectorias y vectores de fuerza.";

    private const string DESC_RIGIDBODY =
        "Simulación 2: Campo de Pruebas de Cuerpos Rígidos\n\n" +
        "Bola interactiva con fricción variable, rampas y colisiones.\n" +
        "Física personalizada sin Rigidbody de Unity.";

    private const string DESC_WAVES =
        "Simulación 3: Comparador de Ondas\n\n" +
        "Ondas de Gerstner vs Sinusoidal en malla de agua.\n" +
        "Boya dinámica con flotabilidad realista.";

    private void Start()
    {
        buttonNewtonian.onClick.AddListener(sceneLoader.LoadNewtonian);
        buttonRigidBody.onClick.AddListener(sceneLoader.LoadRigidBody);
        buttonWaves.onClick.AddListener(sceneLoader.LoadWaves);
        buttonQuit.onClick.AddListener(OnQuit);

        buttonNewtonian.GetComponent<Button>()
            .onClick.AddListener(() => ShowDescription(DESC_NEWTONIAN));
        buttonRigidBody.GetComponent<Button>()
            .onClick.AddListener(() => ShowDescription(DESC_RIGIDBODY));
        buttonWaves.GetComponent<Button>()
            .onClick.AddListener(() => ShowDescription(DESC_WAVES));

        descriptionLabel.text = "Selecciona una simulación para comenzar.";
    }

    private void ShowDescription(string text)
    {
        descriptionLabel.text = text;
    }

    private void OnQuit()
    {
        Application.Quit();
        Debug.Log("[MainMenu] Quit application");
    }
}