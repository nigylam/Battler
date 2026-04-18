using UnityEngine;

public class SquadPreview : MonoBehaviour
{
    [SerializeField] private UnitPreview[] _units;

    public void SetAvailable()
    {
        foreach (var unit in _units)
            unit.SetAvailable();
    }

    public void SetBlocked()
    {
        foreach (var unit in _units)
            unit.SetBlocked();
    }
}
