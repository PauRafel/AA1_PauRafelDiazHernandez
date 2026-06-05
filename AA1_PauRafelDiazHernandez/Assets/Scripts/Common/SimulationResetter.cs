using UnityEngine;

public interface IResettable
{
    void ResetSimulation();
}

public class SimulationResetter : MonoBehaviour
{
    private IResettable[] _resettables;

    private void Awake()
    {
        _resettables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                       as IResettable[];
    }

    public void ResetAll()
    {
        _resettables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                       as IResettable[];

        if (_resettables == null) return;

        foreach (var r in _resettables)
            r.ResetSimulation();
    }
}