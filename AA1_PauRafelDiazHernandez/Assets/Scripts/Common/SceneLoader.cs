using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public const string SCENE_MENU = "MainMenu";
    public const string SCENE_NEWTONIAN = "AA4_1_Newtonian";
    public const string SCENE_RIGIDBODY = "AA4_2_RigidBody";
    public const string SCENE_WAVES = "AA4_3_Waves";

    public void LoadMainMenu()
    {
        SimulationManager.Instance.LoadScene(SCENE_MENU);
    }

    public void LoadNewtonian()
    {
        SimulationManager.Instance.LoadScene(SCENE_NEWTONIAN);
    }

    public void LoadRigidBody()
    {
        SimulationManager.Instance.LoadScene(SCENE_RIGIDBODY);
    }

    public void LoadWaves()
    {
        SimulationManager.Instance.LoadScene(SCENE_WAVES);
    }

    public void LoadSceneByIndex(int index)
    {
        SimulationManager.Instance.LoadScene(
            SceneManager.GetSceneByBuildIndex(index).name
        );
    }
}