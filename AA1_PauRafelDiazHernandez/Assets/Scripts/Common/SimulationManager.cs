using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance { get; private set; }

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PauseSimulation()
    {
        _isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeSimulation()
    {
        _isPaused = false;
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (_isPaused) ResumeSimulation();
        else PauseSimulation();
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        _isPaused = false;
        SceneManager.LoadScene(sceneName);
    }
}