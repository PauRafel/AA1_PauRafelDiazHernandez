using UnityEngine;
using TMPro;

public class VictoryChecker : MonoBehaviour
{
    [Header("References")]
    public BallPhysics ball;
    public TMP_Text victoryLabel;

    [Header("Victory Conditions")]
    public float maxVelocityAtGoal = 0.5f;
    public int maxBorderContacts = 2;

    private int _borderContacts = 0;
    private bool _hasWon = false;

    private void Start()
    {
        if (victoryLabel != null)
            victoryLabel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasWon) return;

        if (other.CompareTag("Goal"))
        {
            if (ball.velocity.magnitude < maxVelocityAtGoal)
                TriggerVictory();
            else
                Debug.Log("[Victory] Ball too fast at goal!");
        }

        if (other.CompareTag("Border"))
        {
            _borderContacts++;
            if (_borderContacts > maxBorderContacts)
                Debug.LogWarning("[Victory] Too many border contacts!");
        }
    }

    private void TriggerVictory()
    {
        _hasWon = true;
        SimulationManager.Instance.PauseSimulation();

        if (victoryLabel != null)
        {
            victoryLabel.gameObject.SetActive(true);
            victoryLabel.text = "¡Nivel completado!";
        }

        Debug.Log("[Victory] Level complete!");
    }

    public void Reset()
    {
        _hasWon = false;
        _borderContacts = 0;
        if (victoryLabel != null)
            victoryLabel.gameObject.SetActive(false);
    }
}